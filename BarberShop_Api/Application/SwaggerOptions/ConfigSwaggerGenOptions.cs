using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Asp.Versioning.ApiExplorer;

namespace BarberShop_Api.Application.SwaggerOptions
{
    public class ConfigSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _ApiDescriptorProvide;

        public void Configure(SwaggerGenOptions options)
        {
            foreach(var desc in _ApiDescriptorProvide)
            {
                options.SwaggerDoc(desc.GroupName);
            }
        }

        public OpenApiInfo CreateOpenApiInfo(ApiVersionDescriptor desc)
        {
            OpenApiInfo openApiInfo = new();
        }
    }
}
