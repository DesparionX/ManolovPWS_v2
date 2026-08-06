# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only .csproj files first — lets Docker cache the restore layer
# separately from source changes, so most rebuilds skip restore entirely.
# AppHost is deliberately excluded: it's the local-dev orchestrator, not part
# of what actually runs in production (Railway's Postgres plugin replaces it).
COPY ManolovPWS_v2.Api/ManolovPWS_v2.Api.csproj ManolovPWS_v2.Api/
COPY ManolovPWS_v2.Domain/ManolovPWS_v2.Domain.csproj ManolovPWS_v2.Domain/
COPY ManolovPWS_v2.Infrastructure/ManolovPWS_v2.Infrastructure.csproj ManolovPWS_v2.Infrastructure/
COPY ManolovPWS_v2.Modules.Contact/ManolovPWS_v2.Modules.Contact.csproj ManolovPWS_v2.Modules.Contact/
COPY ManolovPWS_v2.Modules.Content/ManolovPWS_v2.Modules.Content.csproj ManolovPWS_v2.Modules.Content/
COPY ManolovPWS_v2.Modules.Identity/ManolovPWS_v2.Modules.Identity.csproj ManolovPWS_v2.Modules.Identity/
COPY ManolovPWS_v2.Modules.Projects/ManolovPWS_v2.Modules.Projects.csproj ManolovPWS_v2.Modules.Projects/
COPY ManolovPWS_v2.ServiceDefaults/ManolovPWS_v2.ServiceDefaults.csproj ManolovPWS_v2.ServiceDefaults/
COPY ManolovPWS_v2.Shared/ManolovPWS_v2.Shared.csproj ManolovPWS_v2.Shared/

RUN dotnet restore ManolovPWS_v2.Api/ManolovPWS_v2.Api.csproj

# Now copy everything else (source files) and publish
COPY . .
RUN dotnet publish ManolovPWS_v2.Api/ManolovPWS_v2.Api.csproj -c Release -o /app/publish --no-restore

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "ManolovPWS_v2.Api.dll"]