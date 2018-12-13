using GenericLibrary.Data_Access_Layer;
using GenericLibrary.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace GenericLibrary.Test.Data_Access_Layer
{
    [TestFixture]
    public class MagazineRepositoryTest
    {
        internal MagazineRepository magazineRepository;
        internal Mock<ICSVHandler> mockICSVHandler;
        internal Mock<IRepository<Magazine>> mockMagazineRepository;
        internal Mock<IRepository<Author>> mockAuthorRepository;

        [SetUp]
        public void Setup()
        {
            mockICSVHandler = new Mock<ICSVHandler>();
            mockMagazineRepository = new Mock<IRepository<Magazine>>();
            mockAuthorRepository = new Mock<IRepository<Author>>();
            magazineRepository = new MagazineRepository(mockICSVHandler.Object, mockAuthorRepository.Object);
        }

        [Test]
        public void Check_GetAll_Return_No_Magazine()
        {
            var mockMagazines = new List<Magazine>() { };
            mockMagazineRepository.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineRepository.GetAll();

            //Assert
            Assert.AreEqual(magazines.Count, 0);
        }

        [Test]
        public void Check_GetAll_Return_List_Of_Magazines()
        {
            var mockMagazines = new List<Magazine>() {
                new Magazine()
                {
                    Title="ABC",
                    ISBN="2222-34234-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress = string.Empty } },
                    ReleasedDate ="12.12.2002"
                },
                 new Magazine()
                {
                    Title="XYZ",
                    ISBN="3333-123123-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Jadhav"}  },
                    ReleasedDate ="23.02.2004"
                }
            };

            var csvData = new List<string[]>();
            csvData.Add(new List<string>() { "ABC", "2222-34234-3434", "", "12.12.2002" }.ToArray());
            csvData.Add(new List<string>() { "XYZ", "3333-123123-3434", "test@test.com", "23.02.2004" }.ToArray());

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

            var magazines = magazineRepository.GetAll();

            Assert.AreEqual(magazines.Count, mockMagazines.Count);
        }
    }
}
