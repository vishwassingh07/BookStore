using BusinessLayer.Interfaces;
using CommonLayer.AdminModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }
        [HttpPost("AdminLogin")]
        public ActionResult AdminLogin(AdminLoginModel adminLogin)
        {
            try
            {
                var result = adminBL.AdminLogin(adminLogin);
                if(result != null)
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
