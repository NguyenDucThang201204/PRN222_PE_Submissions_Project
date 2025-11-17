using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PE_PRN232_FA25_NguyenDangKhoi.api.Configuration
{
    public sealed class ODataQueryOptionsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            bool hasEnableQuery =
                context.MethodInfo.GetCustomAttributes(true).Any(a => a.GetType().Name == "EnableQueryAttribute") ||
                (context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                    .Any(a => a.GetType().Name == "EnableQueryAttribute") ?? false);

            if (!hasEnableQuery) return;
            operation.Parameters ??= new List<OpenApiParameter>();

            void Add(string name, string? desc = null) => operation.Parameters.Add(new OpenApiParameter
            {
                Name = name,
                In = ParameterLocation.Query,
                Required = false,
                Description = desc
            });

            Add("$select", "Comma separated properties");
            Add("$filter", "e.g. ModelName eq 'Speedy' and Material eq 'Leather'");
            Add("$orderby", "Property asc|desc");
            Add("$top", "Max items");
            Add("$skip", "Items to skip");
            Add("$count", "true|false");
            Add("$expand", "Navigation properties");
        }
    }
}
