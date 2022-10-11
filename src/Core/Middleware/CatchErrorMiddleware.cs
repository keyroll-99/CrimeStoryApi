using System.Text.Json.Serialization;
using Core.Exceptions;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Middleware;

public class CatchErrorMiddleware
{
    private readonly RequestDelegate _next;

    public CatchErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ServerError e)
        {
            context.Response.StatusCode = e.StatusCode;

            await context.Response.WriteAsync(JsonConvert.SerializeObject((BaseError)e));
        }
        catch (Exception e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(new BaseError("Something went wrong.",
                StatusCodes.Status500InternalServerError)));
        }
    }
}