using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Attribute;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AuthorizeAttribute: System.Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous)
        {
            return;
        }

        if (!ReferenceEquals(context.HttpContext.Items["IsLogged"], "true"))
        {
            throw new ServerError("Unauthorized", StatusCodes.Status401Unauthorized);
        }
    }
}