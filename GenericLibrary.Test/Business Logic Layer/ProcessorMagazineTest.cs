using AutoFixture;
using GenericLibrary.Business_Logic_Layer;
using GenericLibrary.Data_Access_Layer;
using GenericLibrary.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GenericLibrary.Test.Business_Logic_Layer
{
    [TestFixture]
    public class ProcessorMagazineTest
    {
        internal Processor<Magazine> magazineProcessor;
        internal Mock<IRepository<Magazine>> mockMagzineProcessor;

        [SetUp]
        public void Setup()
        {
            mockMagzineProcessor = new Mock<IRepository<Magazine>>();
            magazineProcessor = new Processor<Magazine>(mockMagzineProcessor.Object);
        }

        [Test]
        public void Check_GetAll_Returns_List_Of_Magazine_Is_Null()
        {
            List<Magazine> mockMagazines = null;
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.GetAll();

            //Assert
            Assert.IsNull(magazines);
        }

        [Test]
        public void Check_GetAll_Returns_List_Of_Magazine_Is_Not_Null()
        {
            var mockMagazines = new List<Magazine>() {
                new Magazine()
                {
                    Title="ABC",
                    ISBN="12412-34234-3434",
                    Authors= null,
                    ReleasedDate="23.02.2004"
                },
                 new Magazine()
                {
                    Title="XYZ",
                    ISBN="222-34234-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Jadhav"}  },
                    ReleasedDate="14.06.2005"
                }
            };
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.GetAll();

            //Assert
            Assert.AreEqual(magazines.Count, mockMagazines.Count);
        }

        [Test]
        public void Check_SearchByISBN_Returns_No_List_Of_Magazine()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.ISBN = "2232-1232-1222";

            List<Magazine> mockMagazines = new List<Magazine>() { };
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.SearchByISBN(searchParameterRequest);

            //Assert
            Assert.AreEqual(magazines.Count, 0);
        }

        [Test]
        public void Check_SearchByISBN_Returns_List_Of_Magazine_When_Found_By_ISBN()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.ISBN = "2221-5548-8585";

            var mockMagazines = new List<Magazine>() {
                new Magazine()
                {
                    Title="ABC",
                    ISBN="12412-34234-3434",
                    Authors= null,
                    ReleasedDate="23.02.2004"
                },
                 new Magazine()
                {
                    Title="XYZ",
                    ISBN="2221-5548-8585",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Jadhav"}  },
                    ReleasedDate="14.06.2005"
                }
            };
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.SearchByISBN(searchParameterRequest);

            //Assert
            Assert.AreEqual(1, magazines.Count);
        }

        [Test]
        public void Check_SearchByISBN_Returns_List_Of_Magazine_Null_When_Not_Found_By_ISBN()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.ISBN = "0000-5548-8585";

            var mockMagazines = new List<Magazine>() {
                new Magazine()
                {
                    Title="ABC",
                    ISBN="12412-34234-3434",
                    Authors= null,
                    ReleasedDate="23.02.2004"
                },
                 new Magazine()
                {
                    Title="XYZ",
                    ISBN="222-34234-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Jadhav"}  },
                    ReleasedDate="14.06.2005"
                }
            };
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.SearchByISBN(searchParameterRequest);

            //Assert
            Assert.AreEqual(0, magazines.Count);
        }

        [Test]
        public void Check_SearchByAuthor_Returns_List_Of_Magazine_Is_Null()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "Sandeep";

            var mockMagazines = new List<Magazine>() { };
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.SearchByAuthor(searchParameterRequest);

            //Assert
            Assert.AreEqual(magazines.Count, 0);
        }

        [Test]
        public void Check_SearchByAuthor_Returns_List_Of_Magazine_When_Found_By_Author_FirstName()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "Sandeep";

            var mockMagazines = new List<Magazine>() {
                new Magazine()
                {
                    Title="ABC",
                    ISBN="12412-34234-3434",
                    Authors= new List<Author>(){ },
                    ReleasedDate="23.02.2004"
                },
                 new Magazine()
                {
                    Title="XYZ",
                    ISBN="222-34234-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Jadhav"}  },
                    ReleasedDate="14.06.2005"
                }
            };
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.SearchByAuthor(searchParameterRequest);

            //Assert
            Assert.AreEqual(1, magazines.Count);
        }

        [Test]
        public void Check_SearchByAuthor_Returns_List_Of_Magazine_When_Found_By_Author_LastName()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "Jadhav";

            var mockMagazines = new List<Magazine>() {
                new Magazine()
                {
                    Title="ABC",
                    ISBN="12412-34234-3434",
                    Authors= new List<Author>(){ },
                    ReleasedDate="23.02.2004"
                },
                 new Magazine()
                {
                    Title="XYZ",
                    ISBN="222-34234-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep1" , LastName="Jadhav"}  },
                    ReleasedDate="14.06.2005"
                }
            };
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.SearchByAuthor(searchParameterRequest);

            //Assert
            Assert.AreEqual(1, magazines.Count);
        }

        [Test]
        public void Check_SearchByAuthor_Returns_List_Of_Magazine_When_Found_By_Author_Email_Address()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "test@test.com";

            var mockMagazines = new List<Magazine>() {
                new Magazine()
                {
                    Title="ABC",
                    ISBN="12412-34234-3434",
                    Authors= new List<Author>(){ },
                    ReleasedDate="23.02.2004"
                },
                 new Magazine()
                {
                    Title="XYZ",
                    ISBN="222-34234-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep2" , LastName="Jadhav"}  },
                    ReleasedDate="14.06.2005"
                }
            };
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.SearchByAuthor(searchParameterRequest);

            //Assert
            Assert.AreEqual(1, magazines.Count);
        }

        [Test]
        public void Check_SearchByAuthor_Returns_List_Of_Magazine_Null_When_Not_Found_By_Author_FirstName()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "XYZ";

            var mockMagazines = new List<Magazine>() {
                new Magazine()
                {
                    Title="ABC",
                    ISBN="2221-5548-8585",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="ABC" , LastName="Jadhav"}  },
                    ReleasedDate="23.02.2004"
                },
                 new Magazine()
                {
                    Title="XYZ",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Jadhav"}  },
                    ReleasedDate="14.06.2005"
                }
            };
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.SearchByISBN(searchParameterRequest);

            //Assert
            Assert.AreEqual(0, magazines.Count);
        }

        public void Check_SearchByAuthor_Returns_List_Of_Magazine_Null_When_Not_Found_By_Author_LastName()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "Harley";

            var mockMagazines = new List<Magazine>() {
                new Magazine()
                {
                    Title="ABC",
                    ISBN="2221-5548-8585",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="ABC" , LastName= "Raj" }  },
                    ReleasedDate="23.02.2004"
                },
                 new Magazine()
                {
                    Title="XYZ",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Patil"}  },
                    ReleasedDate="14.06.2005"
                }
            };
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.SearchByISBN(searchParameterRequest);

            //Assert
            Assert.AreEqual(0, magazines.Count);
        }

        public void Check_SearchByAuthor_Returns_List_Of_Magazine_Null_When_Not_Found_By_Author_Email_Address()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "Harley@xyz.in";

            var mockMagazines = new List<Magazine>() {
                new Magazine()
                {
                    Title="ABC",
                    ISBN="2221-5548-8585",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="ABC" , LastName= "Raj" }  },
                    ReleasedDate="23.02.2004"
                },
                 new Magazine()
                {
                    Title="XYZ",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Patil"}  },
                    ReleasedDate="14.06.2005"
                }
            };
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.SearchByISBN(searchParameterRequest);

            //Assert
            Assert.AreEqual(0, magazines.Count);
        }

        public void Check_SortByTitle_Returns_No_List_Of_Magazine()
        {
            List<Magazine> mockMagazines = new List<Magazine>();
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.SortByTitle();

            //Assert
            Assert.AreEqual(0, magazines.Count);
        }

        [Test]
        public void Check_SortByTitle_Returns_List_Of_Magazines()
        {
            var mockMagazines = new List<Magazine>() {
                new Magazine()
                {
                    Title="ZAjwdb",
                    ISBN="2221-5548-8585",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test1@test.com", FirstName="ABC" , LastName="Raj"}  },
                    ReleasedDate="14.06.2005"
                },
                 new Magazine()
                {
                    Title="O'Reillys",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test2@test.com", FirstName="XYZ" , LastName="Patil"}  },
                    ReleasedDate="23.02.2004"
                },
                new Magazine()
                {
                    Title="Hello",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test3@test.com", FirstName="IJB" , LastName="Patil"}  },
                    ReleasedDate="14.06.2009"
                }
            };
            mockMagzineProcessor.Setup(data => data.GetAll()).Returns(mockMagazines);

            var magazines = magazineProcessor.SortByTitle();

            var expected = magazines.OrderBy(x => x.Title).ToList();

            Assert.IsTrue(expected.SequenceEqual(magazines));
        }
    }
}
