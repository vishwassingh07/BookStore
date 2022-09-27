using BusinessLayer.Interfaces;
using CommonLayer.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(string Email)
        {
            try
            {
                var result = userBL.ForgotPassword(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset Link Sent Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Link Could Not Be Generated" });
                }
            }
            catch (System.Exception ex)
            {

                return BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(string NewPassword, string ConfirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                if(userBL.ResetPassword(email, NewPassword, ConfirmPassword))
                {
                    return Ok(new { success = true, message = "Password Reset Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password Could Not Be Reset" });
                }
            }
            catch (System.Exception ex)
            {

                return BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
