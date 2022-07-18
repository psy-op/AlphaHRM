using AlphaHRM.BL;
using AlphaHRM.Intereface;
using AlphaHRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static AlphaHRM.Models.Enums;

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

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            try
            {
                var us = await userservice.Login(req);
                return Ok(us);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Login/UserController");
                return BadRequest(new Response<UserDTO>(Enums.ErrorCodes.ServerError, "Error trying to reach/access server."));
            }
        }

        [HttpPost, Authorize(Roles = "Manager")]
        public async Task<IActionResult> Add([FromBody] UserDTO user)
        {
            try
            {
                var us = await userservice.Create(user);
                return Ok(us);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Add/UserController");
                return BadRequest(new Response<UserDTO>(Enums.ErrorCodes.ServerError, "Error trying to reach/access server."));
            }
        }


        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Get([FromBody] Guid id)
        {
            try
            {
                var us = await userservice.GetUser(id);
                return Ok(us);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Get/UserController");
                return BadRequest(new Response<UserDTO>(Enums.ErrorCodes.ServerError, "Error trying to reach/access server."));
            }
        }


        [HttpPut, Authorize(Roles = "Manager")]
        public async Task<IActionResult> Update([FromBody] UserUpdate user)
        {
            try
            {
                var us = await userservice.Update(user);
                return Ok(us);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Update/UserController");
                return BadRequest(new Response<UserDTO>(Enums.ErrorCodes.ServerError, "Error trying to reach/access server."));
            }
        }


        [HttpDelete, Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            try
            {
                var us = await userservice.Delete(id);
                return Ok(us);

            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Delete/UserController");
                return BadRequest(new Response<UserDTO>(Enums.ErrorCodes.ServerError, "Error trying to reach/access server."));
            }
        }

        [HttpPost, Authorize(Roles = "Manager")]
        public async Task<IActionResult> Getall([FromBody] GetUsersRequest req)
        {
            try
            {
                var us = await userservice.GetAllUsers(req);
                return Ok(us);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at GetAll/UserController");
                return BadRequest(new Response<UserDTO>(Enums.ErrorCodes.ServerError, "Error trying to reach/access server."));
            }
        }
    }
}
