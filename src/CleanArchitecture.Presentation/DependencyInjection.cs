using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Presentation;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(cfr =>
        {
            cfr.AddDefaultPolicy(plc =>
            {
                plc.AllowAnyMethod();
                plc.AllowAnyHeader();
                plc.AllowAnyOrigin();
                plc.WithOrigins();
            });
        });

        services.AddApplication();
        services.AddInfrastructure(configuration);


        return services;
    }
}
