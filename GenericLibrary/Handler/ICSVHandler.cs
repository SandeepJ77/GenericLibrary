using System.Collections.Generic;

namespace GenericLibrary.Data_Access_Layer
{
    public interface ICSVHandler
    {
        IEnumerable<string[]> ReadData(string path);      
    }
}
