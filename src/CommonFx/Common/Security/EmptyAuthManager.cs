using System.Security.Claims;

namespace CommonFx.Common.Security
{
    public class EmptyAuthManager : IAuthManager
    {
        private EmptyAuthManager()
        {
            User = new ClaimsPrincipal(new ClaimsIdentity());
        }

        public ClaimsPrincipal User { get; set; }

        public void SignIn(string username, bool rememberMe)
        {
        }

        public void SignOut()
        {
        }

        public static EmptyAuthManager Instance = new EmptyAuthManager();
    }
}