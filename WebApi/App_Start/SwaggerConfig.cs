using System.Web.Http;
using WebActivatorEx;
using WebApi;
using Swashbuckle.Application;
using System.Reflection;
using System.IO;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "�ʻ����νӿ��ĵ�");
                        var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"bin\\WebApi.xml");
                        c.IncludeXmlComments(path);

                        var path2 = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin\\ViewModel.xml");
                        c.IncludeXmlComments(path2);
                    })
                .EnableSwaggerUi("apis/{*assetPath}",
                    c => { c.InjectJavaScript(Assembly.GetExecutingAssembly(), @"WebApi.Swagger.translator.js"); }); ;
        }


    }
}
