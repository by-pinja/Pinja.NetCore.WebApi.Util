using System.Linq;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pinja.NetCore.WebApi.Util.OrderBy
{
    public class OrderByOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var matchingParameter = context.ApiDescription.ParameterDescriptions.FirstOrDefault(x => x?.Type?.Name == typeof(OrderByQueryString<>).Name);

            var matchingOperation = operation.Parameters?.FirstOrDefault(x => x.Name == matchingParameter?.Name);

            if (matchingOperation != default)
            {
                var schema = new OpenApiSchema()
                {
                    Type = JsonSchemaType.String,
                };

                matchingOperation.Description = "fieldName / fieldName,desc / fieldName,asc";
            }
        }
    }
}