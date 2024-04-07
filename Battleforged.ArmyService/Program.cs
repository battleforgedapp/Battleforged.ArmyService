using System.Security.Claims;
using Battleforged.ArmyService.Application.Armies.Queries;
using Battleforged.ArmyService.Domain.Repositories;
using Battleforged.ArmyService.Helpers;
using Battleforged.ArmyService.Infrastructure.Database;
using Battleforged.ArmyService.Infrastructure.Database.Repositories;
using Battleforged.ArmyService.Json;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
{
    // add our db context connection
    builder.Services.AddDbContext<AppDbContext>(cfg => {
        // TODO: production db connection string when running in prod
        cfg.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        cfg.UseSqlite("Data Source=LocalDatabase.db");
    });
    
    // add our MediatR cqrs pipeline
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,
        typeof(FetchArmiesPagedQuery).Assembly
    ));
    
    // configure the cors policy for development
    builder.Services.AddCors(cfg => {
        cfg.AddDefaultPolicy(plc => plc
            .WithOrigins(builder.Configuration.GetValue<string>("AllowedHosts")!.Split("|"))
            .AllowAnyHeader()
            .AllowAnyMethod()
        );
    });
    
    // configure our authentication
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(cfg => {
            // Authority is the URL of your clerk instance
            cfg.Authority = builder.Configuration["Clerk:Authority"];
            cfg.TokenValidationParameters = new TokenValidationParameters {
                // Disable audience validation as we aren't using it
                ValidateAudience = false,
                NameClaimType = ClaimTypes.NameIdentifier 
            };
            cfg.Events = new JwtBearerEvents() {
                OnTokenValidated = context => {
                    var azp = context.Principal?.FindFirstValue("azp");
                    // AuthorizedParty is the base URL of your frontend.
                    if (string.IsNullOrEmpty(azp) || !azp.Equals(builder.Configuration["Clerk:AuthorizedParty"])) {
                        context.Fail("AZP Claim is invalid or missing");
                    }
                    return Task.CompletedTask;
                }
            };
        });
    
    // setup our repositories
    builder.Services.AddScoped<IArmyRepository, ArmyRepository>();
    builder.Services.AddScoped<IEventOutboxRepository, EventOutboxRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    
    // handle our guid convertors a little better
    // configure newtonsoft to work better with fast-endpoints by configuring the settings better!
    JsonConvert.DefaultSettings = () => new JsonSerializerSettings {
        Formatting = Formatting.Indented,
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        Converters = new List<JsonConverter>() {
            new GuidJsonConverter(),
            new NullableGuidJsonConverter()
        }
    };
    
    // configure out endpoints
    builder.Services.AddAuthorization();
    builder.Services.AddFastEndpoints();
    builder.Services.SwaggerDocument();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCors();
    app
        .UseDefaultExceptionHandler()
        .UseFastEndpoints(cfg => {
            cfg.Versioning.Prefix = "v";
            cfg.Serializer.ResponseSerializer = (rsp, dto, cType, jCtx, ct) => {
                rsp.ContentType = cType;
                return rsp.WriteAsync(JsonConvert.SerializeObject(dto), ct);
            };
            cfg.Serializer.RequestDeserializer = async (req, tDto, jCtx, ct) => {
                using var reader = new StreamReader(req.Body);
                return JsonConvert.DeserializeObject(await reader.ReadToEndAsync(), tDto);
            };
        })
        .UseSwaggerGen();
}

// run our web-app!
app.PreStartup().Run();