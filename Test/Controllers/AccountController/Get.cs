using Application.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Test.Controllers.AccountController
{
    [TestClass]
    public class Get : BaseTest
    {
        private const string REQUEST_URL = "api/account/12345";

        [TestMethod]
        public void SuccessTest()
        {
            getAccountInfoMock.GetSuccessImplementation();
            MakeGetRequest(REQUEST_URL);

            var result = response.Content.ReadAsStringAsync().Result;

            var accountInfo = JsonConvert.DeserializeObject<AccountDto>(result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(500, accountInfo.Balance);

        }

        [TestMethod]
        public void SuccessTest_CustomValue()
        {
            getAccountInfoMock.GetSuccessImplementation(100);
            MakeGetRequest(REQUEST_URL);

            var result = response.Content.ReadAsStringAsync().Result;

            var accountInfo = JsonConvert.DeserializeObject<AccountDto>(result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(100, accountInfo.Balance);

        }

        [TestMethod]
        public void NotFoundTest()
        {
            getAccountInfoMock.GetNotFoundImplementation();
            MakeGetRequest(REQUEST_URL);

            var result = response.Content.ReadAsStringAsync().Result;

            var accountInfo = JsonConvert.DeserializeObject<AccountDto>(result);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual(null, accountInfo);
        }

        [TestMethod]
        public void BadRequestTest()
        {
            getAccountInfoMock.GetBadRequestImplementation();
            MakeGetRequest(REQUEST_URL);

            var result = response.Content.ReadAsStringAsync().Result;

            var accountInfo = JsonConvert.DeserializeObject<AccountDto>(result);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsFalse(accountInfo.Balance.HasValue);

        }
    }
}
