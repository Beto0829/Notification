using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Message.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.Communication.Consumer.Manager;
using Common.Communication.Messages;

namespace Message.Infrastrunture;

public static class Startup
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediator();
        services.AddDomainServices();
        services.AddMapper(); 
        services.AddSwaggers();
        services.AddEndpointsApiExplorer();
        services.AddSettingdServices(config);
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    public static void UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("AllowSpecificOrigin");
        app.UseSwaggers(env);
        app.UseExceptionMiddleware();
        app.UseHttpsRedirection();
    }

    
}
