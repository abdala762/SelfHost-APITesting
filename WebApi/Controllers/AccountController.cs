using Application.UseCases;
using System;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class AccountController : ApiController
    {
        public readonly IGetAccountInfo _getAccountInfo;
        public AccountController(IGetAccountInfo getAccountInfo)
        {
            _getAccountInfo = getAccountInfo;
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var accountInfo = _getAccountInfo.Execute(id);

                if (!accountInfo.Balance.HasValue)
                    return NotFound();

                return Ok(accountInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
