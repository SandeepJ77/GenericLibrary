using GenericLibrary.Models;
using System.Collections.Generic;

namespace GenericLibrary.Business_Logic_Layer
{
    public interface IProcessor<T>
        where T : IEntity
    {
        List<T> GetAll();

        List<T> SearchByISBN(SearchParameter request);

        List<T> SearchByAuthor(SearchParameter request);

        List<T> SortByTitle();
    }
}
