using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Any;

namespace BarberShop_Api.Application.SwaggerOptions
{
    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            ApiDescription apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            if (operation.Parameters == null)
            {
                return;
            }

            foreach(OpenApiParameter parameter in operation.Parameters)
            {
                ApiParameterDescription desc = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                {
                    parameter.Description = desc.ModelMetadata?.Description;
                }

                if (parameter.Schema.Default is null && desc.DefaultValue is not null)
                {
                    parameter.Schema.Default = new OpenApiString(desc.DefaultValue.ToString());
                }

                parameter.Required |= desc.IsRequired;
            }
        }
    }
}
