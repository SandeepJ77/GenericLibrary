using System.Collections.Generic;

namespace GenericLibrary.Models
{
    public class Magazine : ICommon, IEntity
    {
        public string ReleasedDate { get; set; }
        public string Title { get; set; }
        public List<Author> Authors { get; set; }
        public string ISBN { get; set; }
    }
}
