using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Test.UseCaseMocks;
using Unity;

namespace Test
{
    public class BaseTest
    {
        private static IDisposable app;
        private const string BASE_ADDRESS = "http://localhost:12345/";

        //Testing only - HttpClient instances should not be handled like this!!
        protected HttpClient httpClient;
        protected HttpResponseMessage response;

        protected GetAccountInfoMock getAccountInfoMock = GetAccountInfoMock.Instance();

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {

        }

        [TestInitialize]
        public void Initialize()
        {
            //Testing only - HttpClient instances should not be handled like this!!
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(BASE_ADDRESS)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            app.Dispose();
        }

        protected void MakeGetRequest(string uri)
        {
            LoadDependencyInjection();

            //Testing only - HttpClient instances should not be handled like this!!
            using (Task<HttpResponseMessage> requestResponse = httpClient.GetAsync(uri))
            {
                response = requestResponse.Result;
            }
        }

        private void LoadDependencyInjection()
        {
            var useCases = new object[]
            {
                getAccountInfoMock.Mock().Object
            };

            LoadDependencyInjection(useCases);
        }

        private void LoadDependencyInjection(params object[] useCases)
        {
            var container = new UnityContainer();
            foreach (var useCase in useCases)
            {
                //This is only an example - dependency injection can be handled differently
                if (useCase != null)
                    container.RegisterInstance(((System.Reflection.TypeInfo)(useCase.GetType())).ImplementedInterfaces.First(), useCase);
            }

            app = WebApp.Start(BASE_ADDRESS, appbuilder => new Startup(container).Configuration(appbuilder));
        }
    }
}
