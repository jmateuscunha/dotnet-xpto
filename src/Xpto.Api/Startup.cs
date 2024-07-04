using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Xpto.Core.Repositories;
using Xpto.Repository;
using Xpto.Infra.Database.Relational;

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
            //opts.JsonSerializerOptions.Converters.Add(new JsonConverters.BigIntegerConverter());
            opts.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
            opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        services.AddRouting(options => options.LowercaseUrls = true)
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();

        services.AddScoped<IWalletRepository, WalletRepository>();

        services.AddDbContext<XptoDbContext>(opt => opt.UseSqlite(Configuration.GetValue<string>("SqlLiteConnectionStrings")));
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
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
