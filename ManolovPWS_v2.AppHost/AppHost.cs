var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddSqlServer("sql")
    .AddDatabase("ManolovPWS");

var api = builder.AddProject<Projects.ManolovPWS_v2_Api>("manolovpws-v2")
    .WithReference(db)
    .WaitFor(db);

await builder.Build().RunAsync();
