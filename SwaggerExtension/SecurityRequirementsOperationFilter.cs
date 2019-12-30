using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic.SwaggerExtension
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        private readonly SecurityRequirementsOperationFilter<AuthorizeAttribute> filter;

        public SecurityRequirementsOperationFilter(
            bool includeUnauthorizedAndForbiddenResponses = true,
            string securitySchemaName = "oauth2")
        {
            this.filter = new SecurityRequirementsOperationFilter<AuthorizeAttribute>((Func<IEnumerable<AuthorizeAttribute>, IEnumerable<string>>)(authAttributes => authAttributes.Where<AuthorizeAttribute>((Func<AuthorizeAttribute, bool>)(a => !string.IsNullOrEmpty(a.Policy))).Select<AuthorizeAttribute, string>((Func<AuthorizeAttribute, string>)(a => a.Policy))), includeUnauthorizedAndForbiddenResponses, securitySchemaName);
        }

        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            this.filter.Apply(operation, context);
        }
    }
}
