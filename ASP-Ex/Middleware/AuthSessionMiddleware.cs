using ASP_Ex.Data.DAL;
using System.Security.Claims;

namespace ASP_Ex.Middleware
{
    public class AuthSessionMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthSessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, DataAccessor dataAccessor)
        {
            if (context.Session.GetString("auth-user-id") is String userId)
            {
                var user = dataAccessor.UserDao.GetUserById(userId);
                if (user != null)
                {
                    Claim[] claims = new Claim[] {
                    new (ClaimTypes.Sid,        userId),
                    new (ClaimTypes.Email,      user.Email),
                    new (ClaimTypes.Name,       user.Name),
                    new (ClaimTypes.Role,       user.Role ?? "")
                    };

                    context.User = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            claims,
                            nameof(AuthSessionMiddleware)
                        )
                    );
                }
            }
            await _next(context);
        }
    }

    public static class AuthSessionMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthSession(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthSessionMiddleware>();
        }
    }
}
