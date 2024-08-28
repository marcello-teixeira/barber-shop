using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Asp.Versioning.ApiExplorer;

namespace BarberShop_Api.Application.SwaggerOptions
{
    public class ConfigSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _ApiDescriptionProvide;

        // Create an injection of ApiVersionDescriptionProvider
        public ConfigSwaggerGenOptions(IApiVersionDescriptionProvider apiDescriptorProvide)
        {
            _ApiDescriptionProvide = apiDescriptorProvide ?? throw new ArgumentNullException(nameof(apiDescriptorProvide));
        }

        //  Configure the swagger with a API versions and the titles
        public void Configure(SwaggerGenOptions options)
        {
            foreach(var desc in _ApiDescriptionProvide.ApiVersionDescriptions)
            {
                options.SwaggerDoc(desc.GroupName, CreateOpenApiInfo(desc));
            }
        }

        //  Create a OpenApiInfo that contains Title and Version
        public OpenApiInfo CreateOpenApiInfo(ApiVersionDescription desc)
        {
            OpenApiInfo openApiInfo = new()
            {
                Title = "BarberShop",
                Version = desc.ApiVersion.ToString()
            };

            if (desc.IsDeprecated)
            {
                openApiInfo.Description += "(Deprecated)";
            }

            return openApiInfo;

        }
    }
}
