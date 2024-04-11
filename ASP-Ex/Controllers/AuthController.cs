using ASP_Ex.Data.DAL;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Ex.Controllers
{
    [Route("api/auth")]
	[ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataAccessor _dataAccessor;

        public AuthController(DataAccessor dataAccessor)
        {
            this._dataAccessor = dataAccessor;
        }

        [HttpGet]
        public object Get([FromQuery(Name = "e-mail")] String email, String? password)
        {
            var user = _dataAccessor.UserDao.Authorize(email, password ?? "");
            if (user == null)
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new { Status = "Auth Failed" };
            }
            else
            {
                HttpContext.Session.SetString("auth-user-id", user.Id.ToString());
                return user;
            }
        }

        [HttpPost]
        public object Post()
        {
            return new { Status = "POST Works" };
        }

        [HttpPut]
        public object Put()
        {
            return new { Status = "PUT Works" };
        }
    }
}
