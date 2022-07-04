using AlphaHRM.Intereface;
using AlphaHRM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AlphaHRM.Controllers
{
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


        [HttpPost]
        public IActionResult Add(VacationDTO vacy)
        {
            try
            {
                var vc = vacationservice.Create(vacy);
                return Ok(vc);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex,"Error at Add/VacationController");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public IActionResult Get(Guid id)
        {
            try
            {
                var vc = vacationservice.GetVacation(id);
                return Ok(vc);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Get/VacationController");
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public IActionResult Update(VacationDTO vacy)
        {
            try
            {
                var vc = vacationservice.Update(vacy);
                return Ok(vc);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Update/VacationController");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var vc = vacationservice.Delete(id);
                return Ok(vc);

            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Delete/VacationController");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Getall()
        {
            try
            {
                var vc = vacationservice.GetAllVacations();
                return Ok(vc);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at GetAll/VacationController");
                return BadRequest(ex.Message);
            }
        }



    }
}
