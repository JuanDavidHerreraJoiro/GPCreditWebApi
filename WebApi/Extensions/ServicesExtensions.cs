namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.SetIsOriginAllowed(origin => true)
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                    });
            });

            return services;
        }
    }
}
