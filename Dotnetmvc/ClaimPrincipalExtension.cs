using System.Security.Claims;

namespace dotnetcoremorningclass
{
    public static class ClaimPrincipalExtension
    {

        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

    }
}
