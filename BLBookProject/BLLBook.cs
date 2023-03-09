using DALBookProject;
using DALBookProject.Database;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLBookProject
{
    public class BLLBook
    {
        DALBook dalbook = new DALBook();
        public List<BookModel> GetBookList(int? catId= null)
        {
            return dalbook.GetBookList(catId);
        }

        public BookModel GetBook(int id)
        {
            return dalbook.GetBook(id);
        }

        public BookModel CreateBook(BookModel bookModel)
        {
            return dalbook.CreateBook(bookModel);
        }

        public BookModel UpdateBook(int bookId, BookModel updatedBook)
        {
            return dalbook.UpdateBook(bookId, updatedBook);
        }

        
        public Boolean DeleteBook(BookModel bookModel)
        {
            var result = dalbook.DeleteBook(bookModel);
            return result;
        }

        public Boolean IsBookExists(string bookName)
        {
            var result = dalbook.IsBookExists(bookName);
            return result;
        }

        public Boolean IsBookExists(string bookName, int id)
        {
            var result = dalbook.IsBookExists(bookName, id);
            return result;
        }

    }
}
