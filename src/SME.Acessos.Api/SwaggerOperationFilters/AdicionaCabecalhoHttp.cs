using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace SME.Acessos.Api.SwaggerOperationFilters;

[ExcludeFromCodeCoverage]
public class AdicionaCabecalhoHttp : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "x-api-acessos-key",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema { Type = "string" },
            Required = false
        });
    }
}
