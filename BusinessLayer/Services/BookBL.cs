using BusinessLayer.Interfaces;
using CommonLayer.BookModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public BookDetailsModel AddBook(BookDetailsModel bookModel)
        {
            try
            {
                return bookRL.AddBook(bookModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BookResponseModel> GetAllBooks()
        {
            try
            {
                return bookRL.GetAllBooks();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public BookResponseModel UpdateBook(int BookId, BookResponseModel bookModel)
        {
            try
            {
                return bookRL.UpdateBook(BookId, bookModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteBook(int BookId)
        {
            try
            {
                return bookRL.DeleteBook(BookId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public BookResponseModel RetrieveBookById(int BookId)
        {
            try
            {
                return bookRL.RetrieveBookById(BookId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
