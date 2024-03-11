using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
        });

        services.AddFluentEmail("admin@test.com").AddSmtpSender("localhost", 2525);

        return services;
    }
}
