using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientBackend.Api.Interfaces;
using PatientBackend.Api.Models;
using Microsoft.AspNetCore.Http;

namespace PatientBackend.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientsService _patientService;
        private readonly ILoginService _loginService;

        public PatientsController(ILogger<PatientsController> logger, IPatientsService patientService, ILoginService loginService)
        {
            _logger = logger;
            _patientService = patientService;
            _loginService = loginService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> Get([FromHeader] string authorization)
        {
            var token = GetToken(authorization);

            if (authorization == null || !_loginService.IsValidToken(token))
            {
                return ReturnUnauthorizedRequest();
            }
            try
            {
                return new OkObjectResult(await _patientService.GetPatients());
            }
            catch (Exception ex)
            {
                return ReturnBadRequest(ex);
            }
        }   

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{patientId}")]
        public async Task<ActionResult<Patient>> GetByPatientId([FromHeader] string authorization, Guid patientId)
        {
            var token = GetToken(authorization);   

            if (authorization == null || !_loginService.IsValidToken(token))
            {
                return ReturnUnauthorizedRequest();
            }
            try
            {
                return new OkObjectResult(await _patientService.GetPatientById(patientId));
            }
            catch (Exception ex)
            {
                return ReturnBadRequest(ex);
            }
        }

        private string GetToken(string authorization)
        {
            return authorization?.Split(' ')[1] ?? null;
        }

        private ActionResult ReturnBadRequest(Exception ex)
        {
            _logger.LogError($"Exception: {ex.Message}");
            return new BadRequestResult();
        }

        private ActionResult ReturnUnauthorizedRequest()
        {
            _logger.LogError("Unauthorized request");
            return new UnauthorizedResult();
        }
    }
}
