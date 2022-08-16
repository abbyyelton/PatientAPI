using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientBackend.Api.Interfaces;
using PatientBackend.Api.Models;

namespace PatientBackend.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _service;

        public LoginController(ILogger<LoginController> logger, ILoginService service)
        {
            _logger = logger;
            _service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult<string> Login(User user)
        {
            var token = _service.Login(user.Email, user.Password);

            if (token != null)
            {
                return new OkObjectResult(token);
            }
            else
            {
                _logger.LogError($"Unauthorized request by user {user.Email}");
                return new UnauthorizedResult();
            }
        }
    }
}
