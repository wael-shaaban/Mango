using Mango.Services.JwtConfigurations;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
var builder = WebApplication.CreateBuilder(args);
builder.AddJwtConfiguration();
if (builder.Environment.EnvironmentName.ToString().ToLower().Equals("production"))
    builder.Configuration.AddJsonFile("ocelot.Production.json", optional: false, reloadOnChange: true);
else
    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    }); ;
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
await app.UseOcelot();
app.Run();
/*Ocelot as an API Gateway in your .NET project, follow these steps.
 * Ocelot is commonly used to handle:-
 * routing, authentication, 
 * load balancing, and other gateway-level  functionalities for microservices or API projects.*/