using Google.Protobuf.WellKnownTypes;

var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.ProductWarehouse_Api>("productwarehouse-api");

_ = builder.AddProject<Projects.ProductWarehouse_UI>("productwarehouse-ui")
    .WithReference(api);

await builder.Build().RunAsync();
