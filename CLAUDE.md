# ManolovPWS_v2 — Backend Architecture

Personal portfolio/website backend. .NET 10, single-owner system (see [Business rules](#business-rules)),
built as a **modular monolith** with **Clean Architecture** layering, orchestrated locally via **.NET Aspire**.

## Solution layout

```
ManolovPWS_v2.Shared           -> (no deps)            CQRS/Result/Error abstractions, cross-cutting contracts
ManolovPWS_v2.Domain            -> Shared               Entities, value objects, repository/factory contracts
ManolovPWS_v2.ServiceDefaults   -> (no deps)             Aspire shared project (OTel, health checks, resilience)
ManolovPWS_v2.Modules.Contact   -> (no deps)             SCAFFOLD ONLY — no .cs files, not wired into Api
ManolovPWS_v2.Modules.Identity  -> Domain, Shared        Users, auth, roles/permissions, admin
ManolovPWS_v2.Modules.Projects  -> Domain, Shared        Portfolio projects
ManolovPWS_v2.Modules.Content   -> Domain, Modules.Identity, Modules.Projects   Posts, CV builder
ManolovPWS_v2.Infrastructure    -> Domain, Modules.Identity, Shared            EF Core, JWT, auth services
ManolovPWS_v2.Api               -> Infrastructure, all Modules.*, ServiceDefaults, Shared   Controllers, DI wiring, Program.cs
ManolovPWS_v2.AppHost            -> Api                  Aspire orchestration (Postgres container + Api)
```

Dependencies point inward per Clean Architecture: `Domain`/`Shared` have zero references out. Two
deliberate deviations worth knowing about before "fixing" them:

- **`Modules.Content` references `Modules.Identity` and `Modules.Projects` directly**
  ([ManolovPWS_v2.Modules.Content.csproj](ManolovPWS_v2.Modules.Content/ManolovPWS_v2.Modules.Content.csproj))
  because the CV-builder feature reads data from both those modules. There is no integration-event or
  shared-read-model boundary between modules — cross-module reads go straight through a project reference.
- **`Infrastructure` references `Modules.Identity`**
  ([ManolovPWS_v2.Infrastructure.csproj](ManolovPWS_v2.Infrastructure/ManolovPWS_v2.Infrastructure.csproj))
  because `AuthorizationService`/`JwtProvider` in Infrastructure implement interfaces declared in the
  Identity module (`Modules.Identity/User/Auth/...`). Normally Infrastructure shouldn't depend on a
  module; here it's implementing module-declared contracts, so treat Identity's `Auth` namespace as
  quasi-domain.
- **`Modules.Contact` is a placeholder.** It has a `.csproj` (referencing `libphonenumber-csharp`,
  `Scrutor`) but no source files and is never called from `ApplicationInjection.AddApplication()`. Don't
  assume it does anything until it has code and a registration call.

All projects target `net10.0` with `Nullable=enable` and `ImplicitUsings=enable`. No `GlobalUsings.cs`
exists anywhere — every non-BCL type is imported explicitly per file.

## Modular monolith: module internals

Modules are **not** Controllers/Services/Repositories folders — they're **vertical-slice CQRS**, one
file per use case, command/query record and handler together:

```
ManolovPWS_v2.Modules.<X>/
  DependencyInjection/
    DependencyInjection.cs      Add<X>Module() extension method — the module's public entry point
    RegisterHandlers.cs         AddHandlers() — Scrutor assembly scan for ICommandHandler<>/IQueryHandler<>
  <Aggregate>/                  e.g. "Post", "Project", "User"
    Features/<UseCase>/<UseCase>.cs   record Command/Query + its Handler, in one file
    Maps/DataTransferObjects.cs
    Maps/ReadModels.cs
    Shared/ReadModels/*.cs
  Results/<X>AppError.cs        static class of module-specific IError instances
```

Concrete examples:

- Content: `CV/` (CVBuilder + GetUserCV) and `Post/Features/{AddPost, DeletePost, EditPost{Context,Gallery,Thumb,Title,Pin}, GetPosts{All,ById}}`
- Identity: `User/Features/{Admin, DeleteUser, GetUser, ManageTokens, RegisterUser, SignInUser, SignOutUser, UpdateUser}` — `UpdateUser` alone has ~14 single-property commands (`UpdateName.cs`, `UpdateEmail.cs`, `UpdateSkills.cs`, ...)
- Projects: `Project/Features/{AddProject, DeleteProject, GetProjects, UpdateProject}` — `UpdateProject` has 8 single-property commands (Description, Gallery, GitHubUrl, LiveUrl, Name, Stack, State, Thumb)

**When adding a use case**, follow this exact shape: one `Features/<Verb><Noun>/<Verb><Noun>.cs` file
containing a `sealed record ...Command : ICommand<TResponse>` (or `ICommand`/`IQuery<TResponse>`) plus a
`...Handler : ICommandHandler<...>`/`IQueryHandler<...>` class in the same file. Don't split
command/handler into separate files — every existing feature keeps them together.

**Module registration**: each module exposes `services.Add<X>Module()`
(`ManolovPWS_v2.Modules.Identity/DependencyInjection/DependencyInjection.cs` →
`AddIdentityModule()`, similarly `AddContentModule()`, `AddProjectModule()`), which calls
`services.AddHandlers()` from the sibling `RegisterHandlers.cs`. That method uses **Scrutor**
(`services.Scan(...)`) to auto-register every `ICommandHandler<>`/`ICommandHandler<,>`/`IQueryHandler<,>`
in the module's own assembly as `Scoped` — new handlers need no manual DI registration, just implement
the interface. Api wires modules together in
[ManolovPWS_v2.Api/DependencyInjection/ApplicationInjection.cs](ManolovPWS_v2.Api/DependencyInjection/ApplicationInjection.cs):

```csharp
public static IServiceCollection AddApplication(this IServiceCollection services)
{
    services.AddIdentityModule();
    services.AddProjectModule();
    services.AddContentModule();
    return services;
}
```

**Persistence is centralized, not per-module.** There is one `AppDbContext` in `Infrastructure`; modules
don't own their own DbContext or schema. Module isolation is enforced at the C#/repository-interface
level only (`Domain/Contracts/Repositories/*`), not at the database level — there are no per-module
Postgres schemas.

## Controllers

All controllers live in `ManolovPWS_v2.Api/Controllers/` (not inside modules): `AccountController`,
`AdminController`, `AuthController`, `CVController`, `PostsController`, `ProjectsController`,
`UsersController`. See
[ManolovPWS_v2.Api/Controllers/PostsController.cs](ManolovPWS_v2.Api/Controllers/PostsController.cs)
as the reference example. Shape to copy for new controllers/actions:

```csharp
[Route("[controller]")]
[ApiController]
public class PostsController(IDispatcher dispatcher) : ControllerBase
{
    private readonly IDispatcher _dispatcher = dispatcher;

    [ProducesResponseType<IReadOnlyList<PostReadModel>>(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var query = new GetAllPostsQuery();
        var result = await _dispatcher.QueryAsync(query, cancellationToken);
        return result.ToActionResult();
    }
}
```

- Primary-constructor DI, `ControllerBase` (API-only, no views).
- `[Route("[controller]")]` + `[ApiController]`, simple REST routes rooted at the controller name
  (`/Posts`, `/Admin/users/{id}/roles`) — **no API versioning** anywhere (no `/v1/`, no `Asp.Versioning`).
- **No MediatR.** There's a hand-rolled `IDispatcher`/`Dispatcher`
  ([ManolovPWS_v2.Api/Services/Dispatcher.cs](ManolovPWS_v2.Api/Services/Dispatcher.cs)) that resolves
  `ICommandHandler<>`/`IQueryHandler<,>` via reflection (`MakeGenericType` + `dynamic` invocation).
  Controllers only ever call `_dispatcher.SendAsync(cmd, ct)` or `_dispatcher.QueryAsync(query, ct)`,
  never a handler directly.
- **Every action gets `[ProducesResponseType<T>(StatusCodes.Status200OK)]`** (generic form, for actions
  returning a payload) or the non-generic `[ProducesResponseType(StatusCodes.Status200OK)]` (for
  commands with no payload). This was retrofitted across all endpoints in commit `35ced5e` — keep it
  consistent on any new action.
- Authorization is attribute-based: `[Authorize]` (any authenticated user), `[Authorize(Roles =
Roles.Owner)]` (role-gated — `Roles` constants live in
  [ManolovPWS_v2.Shared/Authorization/Roles.cs](ManolovPWS_v2.Shared/Authorization/Roles.cs)), or
  `[AllowAnonymous]` on public GETs and the refresh-token endpoint.
- **No try/catch in controllers.** Every action ends with `return result.ToActionResult();` — success
  and expected-failure HTTP mapping is handled centrally by
  [ManolovPWS_v2.Api/Maps/ResultMaps.cs](ManolovPWS_v2.Api/Maps/ResultMaps.cs), and truly unexpected
  exceptions are caught by the global `IExceptionHandler` chain (see below). Never add a try/catch
  around dispatcher calls in a controller.
- `AuthController.SignInUser`/`RefreshToken` is the one exception to "always call
  `ToActionResult()`" — it manually attaches an `HttpOnly`/`Secure`/`SameSite=None` refresh-token cookie
  scoped to `Path="/Auth"` on the success path.

## Result / error handling pattern

Two parallel error channels — know which one applies:

1. **Expected/business failures** flow through `ITaskResult`/`ITaskResult<T>`
   ([ManolovPWS_v2.Shared/Abstractions/Results/](ManolovPWS_v2.Shared/Abstractions/Results/)) — handlers
   return `Result.Success()`/`Result.Failure(errors)` (or `Result<T>` equivalents) instead of throwing.
   `Value` on a failed `Result<T>` throws `InvalidOperationException` if accessed — always check
   `IsSuccess` first. Errors implement `IError { Message, Code }`
   ([ManolovPWS_v2.Shared/Abstractions/Errors/](ManolovPWS_v2.Shared/Abstractions/Errors/)), with `Code`
   drawn from `ErrorCodes` (`ActionFailed`, `ValidationError`, `Unauthorized`, `Forbidden`, `NotFound`,
   `Conflict`). `ResultMaps.ToActionResult()` switches on the first error's `Code` to pick the HTTP status.
   Module-specific error catalogs live in `Results/<X>AppError.cs` per module (e.g.
   `IdentityAppErrors.UserLimitReached`).
2. **Unexpected/exceptional failures** go through a global `IExceptionHandler` chain registered in
   [ManolovPWS_v2.Api/DependencyInjection/ExceptionHandlersInjection.cs](ManolovPWS_v2.Api/DependencyInjection/ExceptionHandlersInjection.cs):
   `DomainExceptionHandler` → `InfrastructureExceptionHandler` → `GlobalExceptionHandler` (catch-all),
   producing RFC7807 `application/problem+json`. Stack traces/exception details only leak in
   `IsDevelopment()`. Wired via `app.UseExceptionHandler()` in `Program.cs`.

Validation is **not** a separate library — there's no FluentValidation anywhere in the solution.
Validation happens inside domain value-object factories (`Email.Create(...)`, etc.), which throw a typed
`DomainException` subclass on invalid input. `*Request` DTOs under `ManolovPWS_v2.Api/Contracts/` are
plain unvalidated records; the domain layer is the only validation boundary.

## EF Core conventions

Single shared context, not one per module:
[ManolovPWS_v2.Infrastructure/Persistance/AppDbContext.cs](ManolovPWS_v2.Infrastructure/Persistance/AppDbContext.cs):

```csharp
public class AppDbContext : IdentityDbContext<DbUser, IdentityRole<Guid>, Guid>
{
    public override DbSet<DbUser> Users { get; set; }
    public DbSet<DbProject> Projects { get; set; }
    public DbSet<DbPost> Posts { get; set; }
    public DbSet<DbRefreshToken> RefreshTokens { get; set; }
    // OnModelCreating renames AspNetUsers/Roles/etc -> Users/Roles/UserRoles/UserClaims/RoleClaims/UserLogins/UserTokens
    // then builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly)
}
```

- Built on `IdentityDbContext<DbUser, IdentityRole<Guid>, Guid>` — ASP.NET Core Identity is the actual
  user/role/claims store, not a hand-rolled table.
- **Entity configuration = `IEntityTypeConfiguration<T>` classes**, one per entity, in
  `ManolovPWS_v2.Infrastructure/Persistance/Configs/` (`UserConfiguration.cs`, `PostConfiguration.cs`,
  `ProjectConfiguration.cs`, `RefreshTokensConfiguration.cs`), auto-discovered via
  `ApplyConfigurationsFromAssembly`. Do not add inline Fluent API to `OnModelCreating` for new entities —
  add a new `IEntityTypeConfiguration<T>` class instead; `OnModelCreating` is reserved for the Identity
  table renames.
- **Persistence models are separate from domain models.** `DbUser`/`DbPost`/`DbProject`/`DbRefreshToken`
  live in `ManolovPWS_v2.Infrastructure/Persistance/Entities/`, mapped to/from the `Domain` entities via
  extension methods in `ManolovPWS_v2.Infrastructure/Contracts/Maps/{UserExtensions,PostExtensions,ProjectExtensions}.cs`
  (`.ToDomain()`, `.ToDbEntity()`, `.ApplyChanges(domainEntity)`). Never expose `Db*` types outside
  Infrastructure.
- Table names: PascalCase plural, explicit `.ToTable("Posts")`.
- **IDs are never DB-generated** — `.Property(u => u.Id).ValueGeneratedNever()`; IDs come from the domain
  (e.g. `PostId.New()`).
- **Complex value objects are persisted as JSONB**, not relational columns:
  `.HasConversion(v => JsonSerializer.Serialize(...), v => JsonSerializer.Deserialize(...)).HasColumnType("jsonb")`,
  using the shared `JsonOptions.Default` serializer
  ([ManolovPWS_v2.Infrastructure/Persistance/Serialization/JsonOptions.cs](ManolovPWS_v2.Infrastructure/Persistance/Serialization/JsonOptions.cs)).
  Applies to things like `Address`, `Contacts`, `SkillSet`, `Experience`, `EducationHistory`,
  `Certificates`, `ProjectStack`, `PostContent`.
- **Provider: Postgres via Npgsql**, registered in
  [ManolovPWS_v2.Infrastructure/DependencyInjection/DatabaseInjection.cs](ManolovPWS_v2.Infrastructure/DependencyInjection/DatabaseInjection.cs)
  with `options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)` set globally — queries are
  no-tracking by default; opt into tracking explicitly (`.AsTracking()`) only where you intend to mutate
  and `SaveChanges`.
- **Migrations** live in `ManolovPWS_v2.Infrastructure/Persistance/Migrations/`, named
  `{yyyyMMddHHmmss}_{Description}` (existing ones are inconsistently PascalCase vs hyphenated —
  `20260522195559_InitialCreate.cs` vs `20260601173928_Created-RefreshTokens.cs` — prefer PascalCase
  going forward for consistency with C# naming elsewhere in the repo).
- **Design-time factory**:
  [ManolovPWS_v2.Infrastructure/Persistance/AppDbContextFactory.cs](ManolovPWS_v2.Infrastructure/Persistance/AppDbContextFactory.cs)
  implements `IDesignTimeDbContextFactory<AppDbContext>`, loads `.env` via `DotNetEnv.Env.Load()` and
  reads `ConnectionStrings__manolovdb_local` — this is what `dotnet ef migrations add` uses outside the
  Aspire host. Run EF CLI commands from a context where `.env` is present (`ManolovPWS_v2.Api/.env`).
- **Seeding**:
  [ManolovPWS_v2.Infrastructure/Persistance/Seed/IdentitySeeder.cs](ManolovPWS_v2.Infrastructure/Persistance/Seed/IdentitySeeder.cs)
  seeds the four roles (`Owner`, `Admin`, `Moderator`, `User`) via `RoleManager`, invoked from
  `app.SeedDataAsync()` in `Program.cs` before `MapDefaultEndpoints()`.

## Domain layer conventions

- **`IEntity<TKey>`** ([ManolovPWS_v2.Domain/Abstractions/IEntity.cs](ManolovPWS_v2.Domain/Abstractions/IEntity.cs))
  is the only entity abstraction — no `BaseEntity`, no `IAggregateRoot`, no domain-events infrastructure.
- Three aggregates — `User`, `Post`, `Project` — under `Models/<Aggregate>/`. All are **`sealed class` with
  a private constructor**, a static `Create(...)` factory, and a **copy-on-write `With(...)` update
  pattern**: every mutation returns a new instance (`user.UpdateEmail(newEmail) => With(email: newEmail)`)
  rather than mutating in place. Follow this pattern for any new aggregate — don't add public setters or
  in-place mutation methods.
- **Every scalar property is a value object**, one file per concept under `Models/<Aggregate>/Properties/`
  (`Email.cs`, `UserName.cs`, `BirthDate.cs`, `Address.cs`, plus collection value objects like
  `SkillSet`, `Experience`, `EducationHistory`). Value objects: `sealed class`, private constructor,
  static `Create(...)` factory that throws a dedicated `Invalid*Exception` on bad input (e.g.
  `Email.Create` → `InvalidEmailException`). New primitives should follow this shape rather than being
  passed around as raw strings/ints.
- **`DomainException`** ([ManolovPWS_v2.Domain/Errors/DomainException.cs](ManolovPWS_v2.Domain/Errors/DomainException.cs))
  is the abstract base (carries a `Code`); every `Invalid*Exception` derives from it. There's also a
  `DomainError : IError` record type in the same folder that mirrors the `Result`-based error path, but
  it's not the active pattern for domain validation — domain validation throws, it doesn't return
  `DomainError` values. Don't mix the two for the same validation path.
- **Repository/Factory contracts live in Domain, implementations in Infrastructure**:
  `Domain/Contracts/Repositories/{IRepository,IUserRepository,IPostRepository,IProjectRepository}.cs`
  (generic `IRepository<TEntity, TKey> where TEntity : IEntity<TKey>` with `GetAllAsync`,
  `FindByIdAsync`, `SaveAsync`, `RemoveAsync`, `AnyAsync`) and
  `Domain/Contracts/Factories/{IFactory,IUserFactory,IPostFactory,IProjectFactory}.cs`. `IFactory.CreateAsync`
  is insert-only; `IRepository.SaveAsync` is update-only — don't conflate the two when adding a new
  aggregate's persistence contract.

## Shared project (`ManolovPWS_v2.Shared`)

Pure abstractions, zero project references, zero implementation logic:

- `Abstractions/CQRS/{ICommand,ICommandHandler,IQuery,IQueryHandler}.cs` — the CQRS contracts every
  module feature implements.
- `Abstractions/Results/{ITaskResult,Result}.cs` — the `Result`/`Result<T>` pattern described above.
- `Abstractions/Errors/{IError,ErrorCodes}.cs` — `IError` contract + the canonical error code strings.
- `Abstractions/Identity/ICurrentUser.cs` — cross-cutting current-user abstraction, implemented in Api as
  `CurrentUser` ([ManolovPWS_v2.Api/Contracts/Identity/CurrentUser.cs](ManolovPWS_v2.Api/Contracts/Identity/CurrentUser.cs)).
- `Abstractions/Services/IFileStorage.cs` — storage abstraction (contract exists; check current DI
  registrations in Infrastructure before assuming an implementation is wired up).
- `Authorization/{CustomClaimTypes,Permissions,Roles}.cs` — `Roles` (`owner`, `admin`, `moderator`,
  `user`) and fine-grained `Permissions` strings (`posts.create`, `projects.edit`, ...) backing both JWT
  claims and ASP.NET Core policies.

## Identity / Auth

JWT Bearer for API access; refresh token in an HttpOnly cookie for rotation.

- `AddAuthenticationDI()` / `AddAuthorizationDI()` in
  [ManolovPWS_v2.Api/Extensions/{Authentication,Authorization}.cs](ManolovPWS_v2.Api/Extensions/) —
  Authorization dynamically registers **one policy per permission string** in
  `Permissions.AllPermissions`, layered on top of the role-based `[Authorize(Roles=...)]` attributes used
  in controllers. If you add a new `Permissions` constant, it gets a policy automatically — no manual
  policy registration needed.
- Token issuance: `JwtProvider` (`Infrastructure/Contracts/Authentication/JWT/JwtProvider.cs`) implements
  `ITokenProvider` (declared in `Modules.Identity/User/Auth/Token/ITokenProvider.cs`) — signs
  `NameIdentifier`/`Name`/`Email` + one `Role` claim per role + one `permission` claim per permission,
  HMAC-SHA256.
- Refresh tokens: `RefreshTokensService` + `DbRefreshToken` (hash, expiry, revocation,
  `ReplacedByTokenHash` for rotation tracking).
- User store: `UserManager<DbUser>` / `RoleManager<IdentityRole<Guid>>` (standard ASP.NET Core Identity),
  wired via `AddUserIdentity()` in
  [ManolovPWS_v2.Infrastructure/DependencyInjection/IdentityInjection.cs](ManolovPWS_v2.Infrastructure/DependencyInjection/IdentityInjection.cs).
- `AuthorizationService` (`Infrastructure/Contracts/Authorization/AuthorizationService.cs`) does
  claims-based permission grant/revoke and role add/remove — returns `ITaskResult` failures rather than
  throwing for expected cases (duplicate grant, missing user, etc).
- Admin surface: `AdminController`, `[Authorize(Roles = Roles.Owner)]` on every action —
  `GET/POST/DELETE /Admin/users/{id}/roles` and `/Admin/users/{id}/permissions`, backed by
  `Modules.Identity/User/Features/Admin/*`.

### Business rules

- **Single-user system.** `RegisterUserCommandHandler`
  (`ManolovPWS_v2.Modules.Identity/User/Features/RegisterUser/RegisterUser.cs`) rejects registration if
  any user already exists (`IdentityAppErrors.UserLimitReached`). This is a single-owner portfolio
  backend, not multi-tenant — don't design new features assuming multiple independent users/accounts.

## CORS

`AddApiCors()` ([ManolovPWS_v2.Api/Extensions/Cors.cs](ManolovPWS_v2.Api/Extensions/Cors.cs)) binds a
`Cors` config section to `CorsSettings` (`ClientUrl`, `LocalUrl`) and registers a **named** policy
`"Client"`. `Program.cs` must call `app.UseCors("Client")` — calling the parameterless `app.UseCors()`
silently applies the (unset) default policy and disables enforcement; this was a real bug fixed in commit
`cf61678`. Middleware order: `UseHttpsRedirection()` → `UseCors("Client")` →
`UseAuthentication()`/`UseAuthorization()` → `MapControllers()`. Keep new middleware in that relative
order.

## Configuration & secrets

- `appsettings.json`/`appsettings.Development.json` hold only `Logging`/`AllowedHosts`. Actual secrets
  (CORS origins, JWT key/issuer/audience/expiry, Postgres connection string) live in a **gitignored
  `.env`** at `ManolovPWS_v2.Api/.env`, loaded via `DotNetEnv.Env.Load()` at the very top of `Program.cs`,
  using the ASP.NET Core double-underscore binding convention: `JWT__KEY`, `JWT__ISSUER`, `JWT__AUDIENCE`,
  `JWT__EXPIRYMINUTES`, `CORS__ClientUrl`, `CORS__LocalUrl`, `ConnectionStrings__manolovdb_local`. When
  adding new config, follow this `.env` + double-underscore pattern rather than editing
  `appsettings.json`.
- OpenAPI docs use **Scalar** (`Scalar.AspNetCore`), not Swashbuckle — `AddConfiguredOpenApi()`
  ([ManolovPWS_v2.Api/Extensions/OpenApi.cs](ManolovPWS_v2.Api/Extensions/OpenApi.cs)) adds a
  `BearerSecuritySchemeTransformer` so every operation shows Bearer auth in the UI. Only mapped in
  `IsDevelopment()`.

## AppHost / ServiceDefaults (.NET Aspire)

[ManolovPWS_v2.AppHost/AppHost.cs](ManolovPWS_v2.AppHost/AppHost.cs) is the entire orchestration:
provisions a containerized `postgres:16` with a data volume, creates a `manolovdb` database, and starts
the `Api` project with `WithReference(db).WaitFor(db)`. Only `Api` is orchestrated — modules are not
separately hosted, confirming this is a monolith, not microservices.

`ManolovPWS_v2.ServiceDefaults/Extensions.cs` provides the standard Aspire
`AddServiceDefaults()`/`MapDefaultEndpoints()`: OpenTelemetry (logging/metrics/tracing, health-check
paths excluded from traces), service discovery, `AddStandardResilienceHandler()` on all typed
`HttpClient`s, `/health` and `/alive` endpoints (dev-only), and a conditional OTLP exporter when
`OTEL_EXPORTER_OTLP_ENDPOINT` is set. `Api`'s `Program.cs` calls `AddServiceDefaults()` right after
`WebApplication.CreateBuilder` and `MapDefaultEndpoints()` before the dev-only OpenAPI/Scalar wiring.

## Other conventions

- **Records vs classes**: commands/queries are `sealed record` (e.g. `RegisterUserCommand(string
UserName, ...) : ICommand`); domain entities/value objects are `sealed class` with private constructors;
  error types are `sealed record` implementing `IError`.
- Nullable reference types enabled solution-wide; no `#nullable disable` shortcuts expected in new code.
- No FluentValidation, no MediatR, no `Asp.Versioning` — don't introduce these without discussing first,
  since the existing patterns (domain-thrown validation, custom `Dispatcher`, unversioned routes) cover
  their use cases already.
- Logging is plain `ILogger<T>` DI; don't introduce a different logging abstraction.
