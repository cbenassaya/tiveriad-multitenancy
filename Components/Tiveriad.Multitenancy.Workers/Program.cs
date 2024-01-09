using Tiveriad.Multitenancy.Workers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddKeycloak();
builder.Services.AddEip();
builder.Services.AddSubscribers();
builder.Services.AddMultiTenancyWorkerServices();
var host = builder.Build();
host.Run();