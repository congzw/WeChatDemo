using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonFx.Common.Web.Mvc
{
    public abstract class MyControllerBase : Controller
    {
        public MyJsonResult<T> MyJson<T>(T model)
        {
            return this.Request.HttpMethod.NbEquals("GET")
                ? new MyJsonResult<T>() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet }
                : new MyJsonResult<T>() { Data = model };
        }
    }
}
