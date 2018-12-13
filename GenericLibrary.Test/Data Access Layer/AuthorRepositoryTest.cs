using GenericLibrary.Data_Access_Layer;
using GenericLibrary.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace GenericLibrary.Test.Data_Access_Layer
{
    [TestFixture]
    public class AuthorRepositoryTest
    {
        internal AuthorRepository authorRepository;
        internal Mock<ICSVHandler> mockICSVHandler;
        internal Mock<IRepository<Author>> mockAuthorIRepository;

        [SetUp]
        public void Setup()
        {
            mockICSVHandler = new Mock<ICSVHandler>();
            mockAuthorIRepository = new Mock<IRepository<Author>>();
            authorRepository = new AuthorRepository(mockICSVHandler.Object);
        }

        [Test]
        public void Check_GetAll_Return_No_Author()
        {
            List<Author> mockAuthors = null;
            mockAuthorIRepository.Setup(data => data.GetAll()).Returns(mockAuthors);

            var authors = authorRepository.GetAll();

            //Assert
            Assert.AreEqual(authors.Count, 0);
        }

        [Test]
        public void Check_GetAll_Return_List_Of_Authors()
        {
            var mockAuthors = new List<Author>() {
                new Author()
                {
                    EmailAddress="test@test.com",
                    FirstName="Sandeep",
                    LastName="Jadhav"
                },
                new Author()
                {
                    EmailAddress="test1@test.com",
                    FirstName="Ravi",
                    LastName="Patil"
                },
                 new Author()
                {
                    EmailAddress="test2@test.com",
                    FirstName="Hardik",
                    LastName="Raj"
                }
            };

            var csvData = new List<string[]>();
            csvData.Add(new List<string>() { "test@test.com", "Sandeep", "Jadhav" }.ToArray());
            csvData.Add(new List<string>() { "test1@test.com", "Ravi", "Patil" }.ToArray());
            csvData.Add(new List<string>() { "test2@test.com", "Hardik", "Raj" }.ToArray());
            
            mockICSVHandler.Setup(data => data.ReadData(It.IsAny<string>())).Returns(csvData);

            
            var authors = authorRepository.GetAll();
                      
            Assert.AreEqual(authors.Count, mockAuthors.Count);
        }
    }
}
