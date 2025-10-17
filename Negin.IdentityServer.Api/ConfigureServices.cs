using DNTCaptcha.Core;
using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Infrastructure.Persistence;
using IdentityServer.Infrastructure.Services;
using IdentityServer.Services;
using IdentityServer8.Configuration;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Text.Json.Serialization;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services, AppSettings appSetting, IWebHostEnvironment env)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
        services.AddControllersWithViews();
        services.AddControllers().AddNewtonsoftJson();
        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(options =>
        {
            options.AddPolicy(name: "CorsPolicy",
                              policy =>
                              {
                                  policy.AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials()
                                 .WithOrigins(appSetting.CORSTrustedOrigins);
                              });
        });


        LockoutOptions lockoutOptions = new()
        {
            AllowedForNewUsers = true,
            DefaultLockoutTimeSpan = TimeSpan.FromDays(1),
            MaxFailedAccessAttempts = 5
        };

        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(appSetting.ConnectionStrings.MSSQL, sqlserverOptions =>
            {
                sqlserverOptions.CommandTimeout(360); // 3 minutes
                sqlserverOptions.EnableRetryOnFailure(3);
            });
        }, ServiceLifetime.Scoped);

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Lockout = lockoutOptions;
            options.User = new UserOptions { RequireUniqueEmail = false };
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.User.RequireUniqueEmail = false;
            options.SignIn.RequireConfirmedEmail = false;

        })
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultTokenProviders();

        IIdentityServerBuilder builder = services.AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
            options.UserInteraction.LoginUrl = "/Account/Login";
            options.UserInteraction.LogoutUrl = "/Account/Logout";
            options.Authentication = new AuthenticationOptions()
            {
                CookieLifetime = TimeSpan.FromHours(12), // ID server cookie timeout set to 12 hours
                CookieSlidingExpiration = true
            };
        })
        .AddDeveloperSigningCredential()
        .AddConfigurationStore(options =>
        {
            options.ConfigureDbContext = b => b.UseSqlServer(appSetting.ConnectionStrings.MSSQL);
        })
        .AddOperationalStore(options =>
        {
            options.ConfigureDbContext = b => b.UseSqlServer(appSetting.ConnectionStrings.MSSQL);
            options.EnableTokenCleanup = true;
        })
        .AddAspNetIdentity<ApplicationUser>()
        .AddProfileService<ProfileService>();

        services.AddAuthentication().AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.Authority = appSetting.IdentityServerAddress;
                jwt.RequireHttpsMetadata = false;
                jwt.Audience = appSetting.Scope;
                jwt.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
            });

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
            {
                busFactoryConfigurator.Host(appSetting.QueueSettings.HostName, appSetting.QueueSettings.VirtualHost, h =>
                {
                    h.Username(appSetting.QueueSettings.UserName);
                    h.Password(appSetting.QueueSettings.Password);
                });
                busFactoryConfigurator.ExchangeType = ExchangeType.Direct;

            });
            busConfigurator.AddRequestClient<EmailVerificationCodeContract>();
            busConfigurator.AddRequestClient<SmsVerificationCodeContract>();

        });

        services.AddSingleton<IPublishEndpoint>(p => p.GetRequiredService<IBusControl>());
        services.AddSingleton<ISendEndpointProvider>(p => p.GetRequiredService<IBusControl>());
        services.AddSingleton<IBus>(p => p.GetRequiredService<IBusControl>());
        //services.AddMassTransitHostedService();

        services.AddDNTCaptcha(options =>
        {
            // options.UseSessionStorageProvider(); // -> It doesn't rely on the server or client's times. Also it's the safest one.
            // options.UseMemoryCacheStorageProvider(); // -> It relies on the server's times. It's safer than the CookieStorageProvider.
            options
                .UseMemoryCacheStorageProvider(
                                /* If you are using CORS, set it to `None` */)  // -> It relies on the server and client's times. It's ideal for scalability, because it doesn't save anything in the server's memory.
                                                                                // .UseDistributedCacheStorageProvider(); // --> It's ideal for scalability using `services.AddStackExchangeRedisCache()` for instance.
                                                                                // .UseDistributedSerializationProvider();

                // Don't set this line (remove it) to use the installed system's fonts (FontName = "Tahoma").
                // Or if you want to use a custom font, make sure that font is present in the wwwroot/fonts folder and also use a good and complete font!
                .UseCustomFont(Path.Combine(env.WebRootPath, "fonts", "IRANSans(FaNum)_Bold.ttf")).AbsoluteExpiration(1)
                .RateLimiterPermitLimit(
                    10) // for .NET 7x, Also you need to call app.UseRateLimiter() after calling app.UseRouting().
                .ShowExceptionsInResponse(env.IsDevelopment()).ShowThousandsSeparators(false)
                .WithNoise(0.015f, 0.015f, 1, 0.0f).WithEncryptionKey("This is my secure key!")
                .WithNonceKey("NETESCAPADES_NONCE")

                //.WithCaptchaImageControllerRouteTemplate("my-custom-captcha/[action]")
                .InputNames(new DNTCaptchaComponent
                {
                    CaptchaHiddenInputName = "DNTCaptchaText",
                    CaptchaHiddenTokenName = "DNTCaptchaToken",
                    CaptchaInputName = "DNTCaptchaInputText"
                }).Identifier("dntCaptcha");
        });

        services.AddControllers().AddJsonOptions(opt =>
        {
            // This is necessary for the languages enum.
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }
}
