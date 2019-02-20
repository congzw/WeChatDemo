namespace MvcApp.Web.Areas.Demos.Models
{
    public interface IFooAppService
    {
        string SayHi();
    }

    public class FooAppService : IFooAppService
    {
        public string SayHi()
        {
            return "Hi!";
        }
    }
}
