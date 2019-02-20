using System;
using CommonFx.Common.Data.Model;
using CommonFx.Common;

namespace CommonFx.BaseLib.Accounts
{
    public class Account : NbEntity<Account>
    {
        public Account()
        {
            LastUpdate = UtilsDateTime.GetTime();
        }

        public virtual string LoginName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Salt { get; set; }
        public virtual string NickName { get; set; }
        public virtual DateTime LastUpdate { get; set; }
        public virtual string FromSource { get; set; }
        public virtual string ValueBag { get; set; }
        public virtual string Roles { get; set; }
        
        public static string Init = "Init";
        public static string ScanLogin = "ScanLogin";
        public virtual bool IsScanLoginUser()
        {
            return ScanLogin.NbEquals(FromSource);
        }
        public virtual bool IsInitUser()
        {
            return Init.NbEquals(FromSource);
        }
        public virtual bool IsLocalUser()
        {
            return !IsScanLoginUser();
        }
    }
}
