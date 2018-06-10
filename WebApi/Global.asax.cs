using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver { IgnoreSerializableAttribute = true };
            GlobalFilters.Filters.Add(new CustomExceptionAttribute());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
            Exception objExp = HttpContext.Current.Server.GetLastError(); //捕获全局异常

        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public virtual void OnException(ExceptionContext filterContext)
        {
            string message = string.Format("消息类型：{0}<br>消息内容：{1}<br>引发异常的方法：{2}<br>引发异常源：{3}"
                , filterContext.Exception.GetType().Name
                , filterContext.Exception.Message
                 , filterContext.Exception.TargetSite
                 , filterContext.Exception.Source + filterContext.Exception.StackTrace
                 );
        }
    }
}
