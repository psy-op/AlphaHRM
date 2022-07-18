using AlphaHRM.Intereface;
using AlphaHRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AlphaHRM.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        readonly ILogger<VacationController> logger;
        readonly IVacationManager vacationservice;
        public VacationController(IVacationManager vacationservice, ILogger<VacationController> logger)
        {
            this.vacationservice = vacationservice;
            this.logger = logger;
        }


        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Add([FromBody] VacationDTO vacy)
        {
            try
            {
                var vc = await vacationservice.Create(vacy);
                return Ok(vc);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex,"Error at Add/VacationController");
                return BadRequest(new Response<UserDTO>(Enums.ErrorCodes.ServerError, "Error trying to reach/access server."));
            }
        }


        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Get([FromBody] Guid id)
        {
            try
            {
                var vc = await vacationservice.GetVacation(id);
                return Ok(vc);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Get/VacationController");
                return BadRequest(new Response<UserDTO>(Enums.ErrorCodes.ServerError, "Error trying to reach/access server."));
            }
        }


        [HttpPut,Authorize(Roles = "Manager")]
        public async Task<IActionResult> Update([FromBody] VacationUpdate vacy)
        {
            try
            {
                var vc = await vacationservice.Update(vacy);
                return Ok(vc);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Update/VacationController");
                return BadRequest(new Response<UserDTO>(Enums.ErrorCodes.ServerError, "Error trying to reach/access server."));
            }
        }


        [HttpDelete, Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            try
            {
                var vc = await vacationservice.Delete(id);
                return Ok(vc);

            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Delete/VacationController");
                return BadRequest(new Response<UserDTO>(Enums.ErrorCodes.ServerError, "Error trying to reach/access server."));
            }
        }

        [HttpPost, Authorize(Roles = "Manager")]
        public async Task<IActionResult> Getall([FromBody] GetVacationRequest req)
        {
            try
            {
                var vc = await vacationservice.GetAllVacations(req);
                return Ok(vc);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at GetAll/VacationController");
                return BadRequest(new Response<UserDTO>(Enums.ErrorCodes.ServerError, "Error trying to reach/access server."));
            }
        }

    }
}
