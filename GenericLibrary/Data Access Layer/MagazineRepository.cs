using GenericLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericLibrary.Data_Access_Layer
{
    public class MagazineRepository : IRepository<Magazine>
    {
        private readonly ICSVHandler _ICSVHandler;
        private readonly IRepository<Author> _IAuthorRepository;

        private readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"CSVData\" + UtilLibrary.Util.MAGAZINE_CSV;

        public MagazineRepository(ICSVHandler iCSVHandler, IRepository<Author> iAuthorRepository)
        {
            _ICSVHandler = iCSVHandler;
            _IAuthorRepository = iAuthorRepository;
        }
        /// <summary>
        /// Return list of  magazines from CSV file
        /// </summary>
        /// <returns></returns>
        public List<Magazine> GetAll()
        {
            List<Magazine> magazines = null;
            var magazinesData = _ICSVHandler.ReadData(path);

            if (null != magazinesData)
            {
                var authorsData = _IAuthorRepository.GetAll(); // TODO : Need to refactor this call:
                magazines = new List<Magazine>();
                foreach (var magazine in magazinesData)
                {
                    magazines.Add(new Magazine()
                    {
                        Title = magazine[0],
                        ISBN = magazine[1],
                        Authors = magazine[2].Split(',').ToList().Select(x => authorsData.FirstOrDefault(a => a.EmailAddress.Equals(x))).ToList() ?? new List<Author>(),
                        ReleasedDate = magazine[3]
                    });
                }
            }            
            return magazines;
        }
    }
}
