using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DNAAnalysis.API.Filters
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();

                var enumNames = Enum.GetNames(context.Type);

                foreach (var name in enumNames)
                {
                    schema.Enum.Add(new Microsoft.OpenApi.Any.OpenApiString(name));
                }

                schema.Type = "string";
                schema.Format = null;
            }
        }
    }
}