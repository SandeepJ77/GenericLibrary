using GenericLibrary.Business_Logic_Layer;
using GenericLibrary.Data_Access_Layer;
using GenericLibrary.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;

namespace GenericLibrary.Test.Business_Logic_Layer
{
    [TestFixture]
    public class ProcessorBookTest
    {
        internal Processor<Book> bookProcessor;
        internal Mock<IRepository<Book>> mockBookProcessor;

        [SetUp]
        public void Setup()
        {
            mockBookProcessor = new Mock<IRepository<Book>>();
            bookProcessor = new Processor<Book>(mockBookProcessor.Object);
        }

        [Test]
        public void Check_GetAll_Returns_List_Of_Books_Is_Null()
        {
            List<Book> mockBooks = null;
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockBooks);

            var books = bookProcessor.GetAll();

            //Assert
            Assert.IsNull(books);
        }

        [Test]
        public void Check_GetAll_Returns_List_Of_Books_Is_Not_Null()
        {
            var mockBooks = new List<Book>() {
                new Book()
                {
                    Title="ABC",
                    ISBN="12412-34234-3434",
                    Authors= null,
                    Summary="Hello Test summary 1"
                },
                 new Book()
                {
                    Title="XYZ",
                    ISBN="222-34234-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Jadhav"}  },
                    Summary="Hello Test summary 1"
                }
            };
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockBooks);

            var books = bookProcessor.GetAll();

            //Assert
            Assert.AreEqual(books.Count, mockBooks.Count);
        }

        [Test]
        public void Check_SearchByISBN_Returns_No_List_Of_Books()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.ISBN = "2232-1232-1222";

            var mockBooks = new List<Book>() {};
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockBooks);

            var books = bookProcessor.SearchByISBN(searchParameterRequest);

            //Assert
            Assert.AreEqual(books.Count, 0);
        }

        [Test]
        public void Check_SearchByISBN_Returns_List_Of_Books_When_Found_By_ISBN()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.ISBN = "2221-5548-8585";

            var mockbooks = new List<Book>() {
                new Book()
                {
                    Title="ABC",
                    ISBN="2221-5548-8585",
                    Authors= null,
                    Summary="Hello Test summary 1"
                },
                 new Book()
                {
                    Title="XYZ",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Jadhav"}  },
                    Summary="Hello Test summary 2"
                }
            };
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockbooks);

            var books = bookProcessor.SearchByISBN(searchParameterRequest);

            //Assert
            Assert.AreEqual(1, books.Count);
        }

        [Test]
        public void Check_SearchByISBN_Returns_List_Of_Books_Null_When_Not_Found_By_ISBN()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.ISBN = "0000-5548-8585";

            var mockbooks = new List<Book>() {
                new Book()
                {
                    Title="ABC",
                    ISBN="2221-5548-8585",
                    Authors= null,
                    Summary="Hello Test summary 1"
                },
                 new Book()
                {
                    Title="XYZ",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Jadhav"}  },
                    Summary="Hello Test summary 2"
                }
            };
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockbooks);

            var books = bookProcessor.SearchByISBN(searchParameterRequest);

            //Assert
            Assert.AreEqual(0, books.Count);
        }

        [Test]
        public void Check_SearchByAuthor_Returns_List_Of_Books_Is_Null()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "Sandeep";

            var mockBooks = new List<Book>() { };
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockBooks);

            var books = bookProcessor.SearchByAuthor(searchParameterRequest);

            //Assert
            Assert.AreEqual(books.Count, 0);
        }

        [Test]
        public void Check_SearchByAuthor_Returns_List_Of_Books_When_Found_By_Author_FirstName()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "Sandeep";

            var mockbooks = new List<Book>() {
                new Book()
                {
                    Title="ABC",
                    ISBN="2221-5548-8585",
                    Authors= new List<Author>(){ },
                    Summary="Hello Test summary 1"
                },
                 new Book()
                {
                    Title="XYZ",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Jadhav"}  },
                    Summary="Hello Test summary 2"
                }
            };
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockbooks);

            var books = bookProcessor.SearchByAuthor(searchParameterRequest);

            //Assert
            Assert.AreEqual(1, books.Count);
        }

        [Test]
        public void Check_SearchByAuthor_Returns_List_Of_Books_When_Found_By_Author_LastName()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "Jadhav";

            var mockbooks = new List<Book>() {
                new Book()
                {
                    Title="ABC",
                    ISBN="2221-5548-8585",
                    Authors= new List<Author>(){ },
                    Summary="Hello Test summary 1"
                },
                 new Book()
                {
                    Title="XYZ",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep1" , LastName="Jadhav"}  },
                    Summary="Hello Test summary 2"
                }
            };
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockbooks);

            var books = bookProcessor.SearchByAuthor(searchParameterRequest);

            //Assert
            Assert.AreEqual(1, books.Count);            
        }

        [Test]
        public void Check_SearchByAuthor_Returns_List_Of_Books_When_Found_By_Author_Email_Address()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "test@test.com";

            var mockbooks = new List<Book>() {
                new Book()
                {
                    Title="ABC",
                    ISBN="2221-5548-8585",
                    Authors= new List<Author>(){ },
                    Summary="Hello Test summary 1"
                },
                 new Book()
                {
                    Title="XYZ",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep2" , LastName="Jadhav"}  },
                    Summary="Hello Test summary 2"
                }
            };
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockbooks);

            var books = bookProcessor.SearchByAuthor(searchParameterRequest);

            //Assert
            Assert.AreEqual(1, books.Count);
        }

        [Test]
        public void Check_SearchByAuthor_Returns_List_Of_Books_Null_When_Not_Found_By_Author_FirstName()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "XYZ";

            var mockbooks = new List<Book>() {
                new Book()
                {
                    Title="ABC",
                    ISBN="2221-5548-8585",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="ABC" , LastName="Jadhav"}  },
                    Summary="Hello Test summary 1"
                },
                 new Book()
                {
                    Title="XYZ",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Jadhav"}  },
                    Summary="Hello Test summary 2"
                }
            };
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockbooks);

            var books = bookProcessor.SearchByISBN(searchParameterRequest);

            //Assert
            Assert.AreEqual(0, books.Count);
        }

        public void Check_SearchByAuthor_Returns_List_Of_Books_Null_When_Not_Found_By_Author_LastName()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "Harley";

            var mockbooks = new List<Book>() {
                new Book()
                {
                    Title="ABC",
                    ISBN="2221-5548-8585",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="ABC" , LastName="Raj"}  },
                    Summary="Hello Test summary 1"
                },
                 new Book()
                {
                    Title="XYZ",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Patil"}  },
                    Summary="Hello Test summary 2"
                }
            };
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockbooks);

            var books = bookProcessor.SearchByISBN(searchParameterRequest);

            //Assert
            Assert.AreEqual(0, books.Count);
        }

        public void Check_SearchByAuthor_Returns_List_Of_Books_Null_When_Not_Found_By_Author_Email_Address()
        {
            var fixture = new Fixture();
            var searchParameterRequest = fixture.Create<SearchParameter>();
            searchParameterRequest.Author = "Harley@xyz.in";

            var mockbooks = new List<Book>() {
                new Book()
                {
                    Title="ABC",
                    ISBN="2221-5548-8585",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="ABC" , LastName="Raj"}  },
                    Summary="Hello Test summary 1"
                },
                 new Book()
                {
                    Title="XYZ",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test@test.com", FirstName="Sandeep" , LastName="Patil"}  },
                    Summary="Hello Test summary 2"
                }
            };
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockbooks);

            var books = bookProcessor.SearchByISBN(searchParameterRequest);

            //Assert
            Assert.AreEqual(0, books.Count);
        }

        public void Check_SortByTitle_Returns_No_List_Of_Books()
        {            
            var mockBooks = new List<Book>() { };
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockBooks);

            var books = bookProcessor.SortByTitle();

            //Assert
            Assert.AreEqual(0, books.Count);
        }

        [Test]
        public void Check_SortByTitle_Returns_List_Of_Books_Is_Not_Null()
        {
            var mockBooks = new List<Book>() {
                new Book()
                {
                    Title="ZAjwdb",
                    ISBN="2221-5548-8585",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test1@test.com", FirstName="ABC" , LastName="Raj"}  },
                    Summary="Hello Test summary 1"
                },
                 new Book()
                {
                    Title="O'Reillys",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test2@test.com", FirstName="XYZ" , LastName="Patil"}  },
                    Summary="Hello Test summary 2"
                },
                new Book()
                {
                    Title="Hello",
                    ISBN="2222-3423-3434",
                    Authors= new List<Author>(){ new Author() { EmailAddress="test3@test.com", FirstName="IJB" , LastName="Patil"}  },
                    Summary="Hello Test summary 3"
                }
            };
            mockBookProcessor.Setup(data => data.GetAll()).Returns(mockBooks);

            var books = bookProcessor.SortByTitle();

            var expected = books.OrderBy(x => x.Title).ToList();

            Assert.IsTrue(expected.SequenceEqual(books));
        }
    }
}
