using GenericLibrary.Models;
using System.Collections.Generic;

namespace GenericLibrary.Data_Access_Layer
{
    public interface IRepository<T> where T : IEntity
    {
        List<T> GetAll();
    }
}
