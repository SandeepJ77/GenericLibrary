using GenericLibrary.Models;
using System;
using System.Collections.Generic;

namespace GenericLibrary.Data_Access_Layer
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly ICSVHandler _ICSVHandler;
        private readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"CSVData\" + UtilLibrary.Util.AUTHOR_CSV;

        public AuthorRepository(ICSVHandler iCSVHandler)
        {
            _ICSVHandler = iCSVHandler;
        }

        /// <summary>
        /// Return list of authors from CSV file
        /// </summary>
        /// <returns></returns>
        public List<Author> GetAll()
        {
            List<Author> authors = null;
            var authorsData = _ICSVHandler.ReadData(path);

            if(null != authorsData) {
                authors = new List<Author>();
                foreach (var author in authorsData)
                {
                    authors.Add(new Author()
                    {
                        EmailAddress = author[0],
                        FirstName = author[1],
                        LastName = author[2]
                    });
                }
            }
            return authors;
        }
    }
}
