var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ProductWarehouse_UI>("productwarehouse-ui");

builder.Build().Run();
