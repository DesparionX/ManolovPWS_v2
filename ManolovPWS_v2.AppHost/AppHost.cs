var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres:16");

var db = postgres.AddDatabase("manolovdb");

var api = builder.AddProject<Projects.ManolovPWS_v2_Api>("manolovpws-v2")
    .WithReference(db)
    .WaitFor(db);

await builder.Build().RunAsync();
