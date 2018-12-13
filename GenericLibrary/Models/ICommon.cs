using System.Collections.Generic;

namespace GenericLibrary.Models
{
    public interface ICommon
    {
        string Title { get; set; }

        List<Author> Authors { get; set; }

        string ISBN { get; set; }
    }
}
