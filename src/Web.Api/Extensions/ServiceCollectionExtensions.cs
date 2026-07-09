namespace Web.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddSwaggerGeneration(this IServiceCollection services)
    {
        services.AddSwaggerGen(static o => o.CustomSchemaIds(id => id.FullName!.Replace('+', '-')));

        return services;
    }
}
