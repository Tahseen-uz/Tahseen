using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Tahseen.Service.Helpers;

public static class HttpContextHelper
{
    public static IHttpContextAccessor Accessor { get; set; }
    public static HttpContext HttpContext => Accessor?.HttpContext;
    public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
    public static long? UserId => GetClaimValueAsLong("Id");
    public static string Email => GetClaimValue("email");
    public static int? LibraryBranchId => GetClaimValueAsInt("LibraryBranchId");
    public static string UserRole => GetClaimValue("role");

    private static long? GetClaimValueAsLong(string claimType)
    {
        if (long.TryParse(HttpContext?.User?.FindFirst(claimType)?.Value, out var value))
        {
            return value;
        }

        return null;
    }

    private static int? GetClaimValueAsInt(string claimType)
    {
        if (int.TryParse(HttpContext?.User?.FindFirst(claimType)?.Value, out var value))
        {
            return value;
        }

        return null;
    }

    private static string GetClaimValue(string claimType)
    {
        return HttpContext?.User?.FindFirst(claimType)?.Value;
    }
}
