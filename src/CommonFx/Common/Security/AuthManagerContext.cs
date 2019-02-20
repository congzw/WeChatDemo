using System;

namespace CommonFx.Common.Security
{
    public class AuthManagerContext
    {
        public AuthManagerContext(IAuthManager authManager)
        {
            if (authManager == null)
            {
                throw new ArgumentNullException("authManager");
            }
            AuthManager = authManager;
        }

        public IAuthManager AuthManager { get; private set; }

        #region for di extensions

        private static readonly Lazy<AuthManagerContext> _lazyInstance = new Lazy<AuthManagerContext>(() => new AuthManagerContext(EmptyAuthManager.Instance));
        private static Func<AuthManagerContext> _resolve = () => _lazyInstance.Value;
        public static Func<AuthManagerContext> Resolve
        {
            get { return _resolve; }
            set { _resolve = value; }
        }

        #endregion
    }
}
