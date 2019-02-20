using CommonFx.Common;

namespace CommonFx.BaseLib.Auths
{
    public interface IAccountAppService
    {
        MessageResult Validate(ValidateDto model);
        MessageResult Login(string username, bool rememberMe);
        MessageResult Logout(string username);
    }

    public class ValidateDto
    {
        public string Identity { get; set; }
        public string Password { get; set; }
    }
}
