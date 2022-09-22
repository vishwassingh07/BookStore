using BusinessLayer.Interfaces;
using CommonLayer.UserModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public ActionResult UserRegistration(UserDetailsModel userModel)
        {
            try
            {
                var result = userBL.Register(userModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "User Registration Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "User Registration Not Successfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost("Login")]
        public ActionResult Login(UserLoginModel loginModel)
        {
            try
            {
                var result = userBL.UserLogin(loginModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Failed" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
