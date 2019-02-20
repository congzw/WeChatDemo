using System.Security.Claims;
using System.Web;
using System.Web.Security;
using CommonFx.Common;
using CommonFx.Common.Security;

namespace MvcApp.Web._Impls
{
    public class FormAuthManager : IAuthManager
    {
        public FormAuthManager()
        {
            User = HttpContext.Current.User as ClaimsPrincipal;
        }

        public ClaimsPrincipal User { get; set; }

        public void SignIn(string username, bool rememberMe)
        {
            ShowClaims("SignIn begin");
            FormsAuthentication.SetAuthCookie(username, rememberMe);
            ShowClaims("SignIn end");
        }

        public void SignOut()
        {
            ShowClaims("SignIn begin");
            FormsAuthentication.SignOut();
            ShowClaims("SignIn end");
        }

        private readonly IMyLogHelper _myLogHelper = MyLogHelper.Resolve(typeof(FormAuthManager));
        private void ShowClaims(string title)
        {
            _myLogHelper.Log(string.Format("----{0}----", title));
            ClaimsPrincipal principal = HttpContext.Current.User as ClaimsPrincipal;
            if (null != principal)
            {
                foreach (Claim claim in principal.Claims)
                {
                    _myLogHelper.Log("CLAIM TYPE: " + claim.Type + "; CLAIM VALUE: " + claim.Value);
                }
            }
        }
    }
}
