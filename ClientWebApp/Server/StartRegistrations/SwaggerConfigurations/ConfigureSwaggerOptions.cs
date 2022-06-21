using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ClientWebApp.Server.StartRegistrations.SwaggerConfigurations
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Northwind Trader Server Api",
                Contact = new OpenApiContact
                {
                    Email = "urgen0240@gmail.com",
                    Name = "URGEN DORJEE"
                }, License = new OpenApiLicense
                {
                    Name = "Northwind Trader Server Api registered License"
                }
            });
        }
    }
}
