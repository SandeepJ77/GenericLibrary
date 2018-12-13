using GenericLibrary.Data_Access_Layer;
using GenericLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace GenericLibrary.Business_Logic_Layer
{
    public class Processor<T> : IProcessor<T>
        where T : IEntity
    {
        private List<T> csvData;
        private readonly IRepository<T> _iRepository;       
        public Processor(IRepository<T> iRepository)
        {
            _iRepository = iRepository;
        }

        /// <summary>
        /// Return CSV file data of T (Book Or Magazine)
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            if (csvData == null)
                csvData = _iRepository.GetAll();

            return csvData;
        }

        /// <summary>
        /// Search data of T (Book Or Magazine) by ISBN
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<T> SearchByISBN(SearchParameter request)
        {
            csvData = GetAll();     
            if(null != csvData)
                return csvData.Where(x => ((ICommon)x).ISBN.Contains(request.ISBN)).ToList();

            return new List<T>();
        }

        /// <summary>
        ///  Search data of T (Book Or Magazine) by Author first name / last name / email address
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<T> SearchByAuthor(SearchParameter request)
        {
            csvData = GetAll();
            if (null != csvData)
                return csvData.Where(x => ((ICommon)x).Authors.Any(y => y.FirstName.ToLower().Contains(request.Author.ToLower()) 
                        || y.LastName.ToLower().Contains(request.Author.ToLower()) 
                        || y.EmailAddress.ToLower().Contains(request.Author.ToLower()))).ToList();

            return new List<T>();
        }

        /// <summary>
        /// Sort data of T (Book Or Magazine) by title
        /// </summary>
        /// <returns></returns>
        public List<T> SortByTitle()
        {
            csvData = GetAll();
            if (null != csvData)
                return csvData.OrderBy(x=> ((ICommon)x).Title).ToList();

            return new List<T>();
        }
    }
}
