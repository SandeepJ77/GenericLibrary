using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GenericLibrary.Data_Access_Layer
{
    public class CSVHandler : ICSVHandler
    {
        /// <summary>
        /// Process CSV file and return data
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IEnumerable<string[]> ReadData(string path)
        {
            var csvFileData = File.ReadAllLines(path).Select(a => a.Split(';'));
            var result = csvFileData.Select(x => x).Skip(1); // Skip used to remove 

            return result;
        }
    }
}
