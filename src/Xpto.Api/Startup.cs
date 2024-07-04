using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xpto.Application.Commands;
using Xpto.Application.Dtos;
using Xpto.Application.Queries;
using Xpto.Core.Repositories;
using Xpto.Infra.Database.Relational;
using Xpto.Repository;

namespace Xpto.Api;

public class Startup
{
    private IWebHostEnvironment Env { get; set; }
    public IConfiguration Configuration { get; }

    public Startup(IWebHostEnvironment hostEnvironment)
    {
        var builder = new ConfigurationBuilder()
                            .SetBasePath(hostEnvironment.ContentRootPath)
                            .AddJsonFile("appsettings.json", true, true)
                            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                            .AddEnvironmentVariables();

        this.Configuration = builder.Build();

        this.Env = hostEnvironment;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddMvc(options =>
        {
            options.EnableEndpointRouting = false;
            //options.Filters.Add(typeof(ValidateModelStateAttribute));
        }).AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.AllowTrailingCommas = true;
            opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());            
            opts.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
            opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        services.AddRouting(options => options.LowercaseUrls = true)
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();

        services.AddScoped<IWalletRepository, WalletRepository>();

        services.AddDbContext<XptoDbContext>(opt => opt.UseSqlite(Configuration.GetValue<string>("SqlLiteConnectionStrings")));

        services.AddMediatR(cfg => {cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);});
        services.AddScoped<IRequestHandler<CreateWalletCommand>, CreateWalletCommandHandler>();
        services.AddScoped<IRequestHandler<GetWalletsQueries, IEnumerable<WalletsDto>>, GetWalletsQueriesHandler>();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, XptoDbContext context)
    {
        //OBSERVE
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();
        app.UseHttpsRedirection();
        //app.UseMiddleware<ExceptionHandlerMiddleware>();
        //app.UseAuthentication();
        //app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
