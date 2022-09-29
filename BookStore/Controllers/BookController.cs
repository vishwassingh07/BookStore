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
        [Authorize]
        [HttpGet("GetAllBooks")]
        public ActionResult GetAllBooks()
        {
            try
            {
                var result = bookBL.GetAllBooks();
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "Books Details Fetched Successfully" , data = result});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Books Details Could Not Be Fetched" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost("UpdateBooks")]
        public ActionResult UpdateBook(int BookId, BookResponseModel bookModel)
        {
            try
            {
                var result = bookBL.UpdateBook(BookId, bookModel);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "Books Updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Books Could Not Be Updated" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteBook")]
        public ActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = bookBL.DeleteBook(BookId);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "Books Deleted Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Books Could Not Be Deleted" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet("GetBookById")]
        public ActionResult RetriveByBookId(int BookId)
        {
            try
            {
                var result = bookBL.RetrieveBookById(BookId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Details Fetched Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Book Details Could Not Be Fetched" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
