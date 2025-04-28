using System.Web.Http;
using WebActivatorEx;
using filmwebManager;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace filmwebManager
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "filmwebManager");
                })
                .EnableSwaggerUi(c =>
                {
                });
        }
    }
}