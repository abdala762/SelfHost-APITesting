using Owin;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Unity;
using Unity.WebApi;
using WebApi.Controllers;

namespace Test
{
    public class Startup
    {
        private readonly IUnityContainer container;

        public Startup(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// This is the OWIN Startup class.
        /// You can use the Configuration method to configure your application, similar to WebApiConfig.cs
        /// </summary>
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            //config.Filters.Add(new MyFilter());

            config.Services.Replace(typeof(IAssembliesResolver), new AssembliesResolver());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.DependencyResolver = new UnityDependencyResolver(container);

            appBuilder.UseWebApi(config);
        }

        internal class AssembliesResolver : DefaultAssembliesResolver
        {
            public override ICollection<Assembly> GetAssemblies()
            {
                var assemblyList = new List<Assembly>
                {
                    { typeof(AccountController).Assembly }
                };

                return assemblyList;
            }
        }
    }
}
