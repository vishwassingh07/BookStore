using BusinessLayer.Interfaces;
using CommonLayer.AddressModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("Controller")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }
        [Authorize]
        [HttpPost("AddAddress")]
        public ActionResult AddAddress(AddressPostModel addressModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = addressBL.AddAddress(UserID, addressModel);
                if(result != null)
                {
                    return this.Ok(new { success = true, Message = "Address Added Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Address Could Not Be Added" });

                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
