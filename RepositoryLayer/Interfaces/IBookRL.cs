using CommonLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        public BookDetailsModel AddBook(BookDetailsModel bookModel);
        public List<BookResponseModel> GetAllBooks();
        public BookResponseModel UpdateBook(int BookId, BookResponseModel bookModel);
        public bool DeleteBook(int BookId);
    }
}
