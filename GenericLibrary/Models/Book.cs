
using System.Collections.Generic;

namespace GenericLibrary.Models
{
    public class Book : ICommon, IEntity
    {
        public string Summary { get; set; }
        public string Title { get; set; }
        public List<Author> Authors { get; set; }
        public string ISBN { get; set; }
    }
}
