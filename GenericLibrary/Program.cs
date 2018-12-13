using GenericLibrary.Business_Logic_Layer;
using GenericLibrary.Data_Access_Layer;
using GenericLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace GenericLibrary
{
    class Program
    {
        /// <summary>
        /// Main Program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                IUnityContainer container = InitalizeContainer();
                var book = container.Resolve<Processor<Book>>();
                var magazine = container.Resolve<Processor<Magazine>>();

                ProcessInputs(book, magazine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured : " + ex.StackTrace);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Initalize Unity Container
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer InitalizeContainer()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<ICSVHandler, CSVHandler>();
            container.RegisterType<IRepository<Author>, AuthorRepository>();
            container.RegisterType<IRepository<Book>, BookRepository>();
            container.RegisterType<IRepository<Magazine>, MagazineRepository>();
            container.RegisterType<IProcessor<Book>, Processor<Book>>();
            container.RegisterType<IProcessor<Magazine>, Processor<Magazine>>();

            container.Resolve<CSVHandler>();
            container.Resolve<IRepository<Author>>();
            container.Resolve<IRepository<Book>>();
            container.Resolve<IRepository<Magazine>>();

            return container;
        }

        /// <summary>
        /// Process user inputs:
        /// </summary>
        /// <param name="book"></param>
        /// <param name="magazine"></param>
        private static void ProcessInputs(Processor<Book> book, Processor<Magazine> magazine)
        {
            var blnToContinue = true;
            while (blnToContinue)
            {
                Console.WriteLine("\nPlease enter your choice below: \n\n" +
                                "1: Print out all details of all books and magazines\n" +
                                "2: Find and print out the details of a book or magazine by searching with an ISBN\n" +
                                "3: Find and print out the details of a book or magazine for an author\n" +
                                "4: Sort all books and magazine by title and print out the result\n" +
                                "5: To exit from program...\n");

                switch (Console.ReadLine().ToString())
                {
                    case "1":
                        PrintResults(new Tuple<List<Book>, List<Magazine>>(book.GetAll(), magazine.GetAll()));
                        break;
                    case "2":
                        Console.WriteLine("Please search by ISBN number\n");
                        SearchParameter reqISBN = new SearchParameter() { ISBN = Console.ReadLine().ToString() };
                        PrintResults(new Tuple<List<Book>, List<Magazine>>(book.SearchByISBN(reqISBN), magazine.SearchByISBN(reqISBN)));
                        break;
                    case "3":
                        Console.WriteLine("Please search Author by first name / last name / email address\n");
                        SearchParameter reqAuthor = new SearchParameter() { Author = Console.ReadLine().ToString() };
                        PrintResults(new Tuple<List<Book>, List<Magazine>>(book.SearchByAuthor(reqAuthor), magazine.SearchByAuthor(reqAuthor)));
                        break;
                    case "4":
                        Console.WriteLine("Please search by Title\n");
                        PrintResults(new Tuple<List<Book>, List<Magazine>>(book.SortByTitle(), magazine.SortByTitle()));
                        break;
                    case "5":
                        blnToContinue = false;
                        break;
                    default:
                        Console.WriteLine("Please choose valid option to continue...\n\n**************************************************************************************************");
                        break;
                }
            }
        }

        /// <summary>
        /// Print Book / Magazine Results
        /// </summary>
        /// <param name="data"></param>
        private static void PrintResults(Tuple<List<Book>, List<Magazine>> data)
        {
            if (null != data)
            {
                #region Books Details
                if (null != data.Item1 && data.Item1.Count > 0)
                    Console.WriteLine("\nBooks Details:");
                else
                    Console.WriteLine("\nBooks not found...");

                foreach (var book in data.Item1)
                {
                    Console.WriteLine(string.Format("Title={0} ISBN-Number={1} Author={2} Summary={3}", book.Title, book.ISBN, string.Join(", ", book.Authors.Select(x => x.DisplayName)), book.Summary));
                }
                #endregion

                #region Magazines Details
                if (null != data.Item2 && data.Item2.Count > 0)
                    Console.WriteLine("\nMagazines Details:");
                else
                    Console.WriteLine("\nMagazines not found...");

                foreach (var magazine in data.Item2)
                {
                    Console.WriteLine(string.Format("Title={0} ISBN-Number={1} Author={2} ReleasedDate={3}", magazine.Title, magazine.ISBN, string.Join(", ", magazine.Authors.Select(x => x.DisplayName)), magazine.ReleasedDate));
                }
                #endregion

                Console.WriteLine("\n\n**************************************************************************************************");
            }
        }
    }
}
