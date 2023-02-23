using Tiveriad.Multitenancy.Apis;
using Tiveriad.Multitenancy.Applications;
using Tiveriad.Multitenancy.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMapper();
builder.Services.AddFilters();
builder.Services.AddController();
builder.Services.AddSwagger();
builder.Services.AddCorsMethod();
builder.Services.AddUserResolverService();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.DatabaseEnsureCreated();
builder.Services.AddEip();
var app = builder.Build();
app.UseDevelopmentEnvironment();
app.UseLoggerFile();
app.UseRouting();
app.UseCorsAllowAny();
app.UseEndpoints();
app.Run();
namespace Tiveriad.Multitenancy.Apis
{
    public partial class Program
    {
    }
}