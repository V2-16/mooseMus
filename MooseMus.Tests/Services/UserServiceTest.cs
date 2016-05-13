using Microsoft.VisualStudio.TestTools.UnitTesting;
using MooseMus.Services;
using MooseMus.Tests;
using MooseMus.Models.Entities;


namespace MooseMus.Tests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private UserService _service;
        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDataContext();
            var f1 = new UserModel
            {
                ID = 1,
                name = "Fanney Ásta Águstsdóttir",
                email = "fanney@ru.is",
                ssn = "2110805319"
            };
            mockDb.user.Add(f1);
            var f2= new UserModel
            {
                ID = 2,
                name = "Snædís Kjartansdóttir",
                email = "snaedis@ru.is",
                ssn = "1812824369"
            };
            mockDb.user.Add(f2);
            var f3 = new UserModel
            {
                ID = 4,
                name = "Rakel Sigurjónsdóttir",
                email = "rakels13@ru.is",
                ssn = "2405853249"
            };
            mockDb.user.Add(f3);

            _service = new UserService(mockDb);
            
        }

        //rename testmethod to fit function to be tested
        [TestMethod]
        public void findUserIdByNameTest()
        {
            //Prepping-making test data
            // Arrange:
            const string user = "Fanney Ásta Águstsdóttir";

            // run the function to be tested - usually one line
            // Act:
            var result = _service.getUserIDByUserName(user);

            // Is the result/response correct
            // Assert:
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void findUserIdByName2Test()
        {
            //Prepping-making test data
            // Arrange:
            const string user = "Rakel Kjartansdóttir";

            // run the function to be tested - usually one line
            // Act:
            var result = _service.getUserIDByUserName(user);

            // Is the result/response correct
            // Assert:
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void findUserIdBySSNTest()
        {
            //Prepping-making test data
            // Arrange:
            const string user = "2405853249";

            // run the function to be tested - usually one line
            // Act:
            var result = _service.getUserIDByUserSSN(user);

            // Is the result/response correct
            // Assert:
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void findUserIdBySSN2()
        {
            //Prepping-making test data
            // Arrange:
            const string user = "2233445566";

            // run the function to be tested - usually one line
            // Act:
            var result = _service.getUserIDByUserSSN(user);

            // Is the result/response correct
            // Assert:
            Assert.AreEqual("Database Error", result);
        }
    }
}
