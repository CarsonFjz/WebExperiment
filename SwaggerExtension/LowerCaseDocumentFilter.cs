using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.SwaggerExtension
{
    public sealed class LowerCaseDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = (IDictionary<string, PathItem>)swaggerDoc.Paths.ToDictionary<KeyValuePair<string, PathItem>, string, PathItem>((Func<KeyValuePair<string, PathItem>, string>)(x => x.Key.ToLower()), (Func<KeyValuePair<string, PathItem>, PathItem>)(x => x.Value));
        }
    }
}
