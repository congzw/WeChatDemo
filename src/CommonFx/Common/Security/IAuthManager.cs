using System.Security.Claims;

namespace CommonFx.Common.Security
{
    public interface IAuthManager
    {
        ClaimsPrincipal User { get; set; }
        void SignIn(string username, bool rememberMe);
        void SignOut();
    }
}
