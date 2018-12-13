using GenericLibrary.Data_Access_Layer;
using GenericLibrary.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace GenericLibrary.Test.Data_Access_Layer
{
    [TestFixture]
    public class BookRepositoryTest
    {
        internal BookRepository bookRepository;
        internal Mock<ICSVHandler> mockICSVHandler;
        internal Mock<IRepository<Book>> mockBookRepository;
        internal Mock<IRepository<Author>> mockAuthorRepository;

        [SetUp]
        public void Setup()
        {
            mockICSVHandler = new Mock<ICSVHandler>();
            mockBookRepository = new Mock<IRepository<Book>>();
            mockAuthorRepository = new Mock<IRepository<Author>>();
            bookRepository = new BookRepository(mockICSVHandler.Object, mockAuthorRepository.Object);
        }

        [Test]
        public void Check_GetAll_Return_No_Book()
        {
            var mockBooks = new List<Book>() { };
            mockBookRepository.Setup(data => data.GetAll()).Returns(mockBooks);

            var books = bookRepository.GetAll();

            //Assert
            Assert.AreEqual(books.Count, 0);
        }

        [Test]
        public void Check_GetAll_Return_List_Of_Books()
        {
            var mockBooks = new List<Book>() {
                new Book()
                {
                    Title="ABC",
                    ISBN="12412-34234-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress = string.Empty } },
                    Summary="Hello Test summary 1"
                },
                 new Book()
                {
                    Title="XYZ",
                    ISBN="222-34234-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Jadhav"}  },
                    Summary="Hello Test summary 2"
                }
            };

            var csvData = new List<string[]>();
            csvData.Add(new List<string>() { "ABC", "12412-34234-3434", "", "Hello Test summary 1" }.ToArray());
            csvData.Add(new List<string>() { "XYZ", "222-34234-3434", "test@test.com", "Hello Test summary 1" }.ToArray());

            var mockAuthors = new List<Author>() {
                new Author()
                {
                    EmailAddress="test@test.com",
                    FirstName="Sandeep",
                    LastName="Jadhav"
                },
                new Author()
                {
                    EmailAddress = string.Empty
                }
            };

            mockAuthorRepository.Setup(data => data.GetAll()).Returns(mockAuthors);
            mockICSVHandler.Setup(data => data.ReadData(It.IsAny<string>())).Returns(csvData);

            var books = bookRepository.GetAll();

            Assert.AreEqual(books.Count, mockBooks.Count);
        }
    }
}
