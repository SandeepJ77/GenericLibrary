using GenericLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericLibrary.Data_Access_Layer
{
    public class BookRepository : IRepository<Book>
    {
        private readonly ICSVHandler _ICSVHandler;
        private readonly IRepository<Author> _IAuthorRepository;
        private readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"CSVData\" + UtilLibrary.Util.BOOK_CSV;
        
        public BookRepository(ICSVHandler iCSVHandler, IRepository<Author> iAuthorRepository) 
        {
            _ICSVHandler = iCSVHandler;
            _IAuthorRepository = iAuthorRepository;
        }

        /// <summary>
        /// Return list of books from CSV file
        /// </summary>
        /// <returns></returns>
        public List<Book> GetAll()
        {
            List<Book> books = null;
            var booksData = _ICSVHandler.ReadData(path);        
            
            if(null != booksData)
            {
                var authorData = _IAuthorRepository.GetAll(); // TODO : Need to refactor this call:
                books = new List<Book>();
                foreach (var book in booksData)
                {
                    books.Add(new Book()
                    {
                        Title = book[0],
                        ISBN = book[1],
                        Authors = book[2].Split(',').ToList().Select(x => authorData.FirstOrDefault(a => a.EmailAddress.Equals(x))).ToList() ?? new List<Author>(),
                        Summary = book[3],
                    });
                }
            }            
            return books;
        }
    }
}
