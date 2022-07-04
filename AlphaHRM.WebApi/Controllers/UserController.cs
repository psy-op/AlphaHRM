using AlphaHRM.Intereface;
using AlphaHRM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AlphaHRM.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly ILogger<UserController> logger;
        readonly IUserManager userservice;
        public UserController(IUserManager userservice, ILogger<UserController> logger)
        {
            this.userservice = userservice;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Login(Guid id, string pass)
        {
            try
            {
                var us = userservice.Login(id,pass);
                return Ok(us);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Login/UserController");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add(UserDTO user)
        {
            try
            {
                var us=userservice.Create(user);
                return Ok(us);
            }
            catch(Exception ex)
            {
                logger.LogCritical(ex, "Error at Add/UserController");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public IActionResult Get(Guid id)
        {
            try
            {
                var us = userservice.GetUser(id);
                return Ok(us);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Get/UserController");
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public IActionResult Update(UserDTO user)
        {
            try
            {
                var us = userservice.Update(user);
                return Ok(us);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Update/UserController");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var us =userservice.Delete(id);
                return Ok(us);

            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Delete/UserController");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Getall()
        {
            try
            {
                var us = userservice.GetAllUsers();
                return Ok(us);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at GetAll/UserController");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Page(int size, int page)
        {
            try
            {
                var us = userservice.Paging(size,page);
                return Ok(us);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Page/UserController");
                return BadRequest(ex.Message);
            }
        }




    }
}
