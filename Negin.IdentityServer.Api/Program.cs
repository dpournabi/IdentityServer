using IdentityServer.Infrastructure.Common;
using IdentityServer.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen.ConventionalRouting;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

AppSettings setting = builder.Configuration.Get<AppSettings>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Define the OAuth2 Scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        //Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",

        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri($"{setting.IdentityServerAddress}/connect/token"),
                Scopes = ScopeStore.GetApiScopesDictionary(),
            }
        }
    });

    // Require the oauth2 scheme for all operations
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                   Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new List<string>
                {
                    "app.shipping",
                   // "app.governorate"
                }
                //ScopeStore.GetApiScopes().Select(x => x.Name).ToArray()
            }
        });
});

builder.Services.AddSwaggerGenWithConventionalRoutes();
builder.Services.AddApplicationServices();
builder.Logging.AddEventLog(); //Set provider as the Event Viewer
builder.Services.AddInfrastructureServices(setting);
builder.Services.AddWebUIServices(setting, builder.Environment);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.AddServerHeader = false;
    serverOptions.ConfigureEndpointDefaults(listenOptions =>
    {
    });
});

builder.Services.AddScoped<ApplicationDbContextInitialiser>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMigrationsEndPoint();

// Initialise and seed database
//ApplicationDbContextInitialiser.MigrateDatabase(app);
//ApplicationDbContextInitialiser.SeedAsync(app);

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nsp.Auth.Api v1");

    //Shipping
    c.OAuthClientId("6bd073b8-cf26-43de-bac8-2f9f291cd56a");
    c.OAuthClientSecret("V4wAzzkxo0ueyLwDLnVRhw");

    //Governorate
    //c.OAuthClientId("7868d015-6c79-4da4-b789-8b4d40611946");
    //c.OAuthClientSecret("V4wAzzkxo0ueyLwDLnVRhw");

    c.OAuthAppName("Swagger UI for My API");
    c.OAuthUsePkce();
    c.RoutePrefix = string.Empty;
});

app.UseIdentityServer();
//app.UseHealthChecks("/health");
app.UseStaticFiles();

app.UseRouting();
app.UseCors("CorsPolicy");
app.UseRateLimiter();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/Administrator/{action=Index}/{id?}");

app.MapRazorPages();
app.UseAuthorization();

//app.UseResponseCompression();
app.Run();