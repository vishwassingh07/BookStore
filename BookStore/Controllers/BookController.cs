using BusinessLayer.Interfaces;
using CommonLayer.BookModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddBook")]
        public ActionResult AddBook(BookDetailsModel bookModel)
        {
            try
            {
                var result = bookBL.AddBook(bookModel);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added Successfully"});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Book Could Not Be Added" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
