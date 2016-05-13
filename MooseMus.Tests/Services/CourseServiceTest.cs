using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MooseMus.Models.Entities;
using MooseMus.Services;

namespace MooseMus.Tests.Services
{
    [TestClass]
    public class CourseServiceTest
    {
        private CourseService _service;
        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDataContext();
            var f1 = new CourseModel
            {
                Id = 21,
                name = "Vefforritun",
                semester = "Fall 2015",
                school = "Computer Science"
            };

            var f2 = new CourseModel
            {
                Id = 22,
                name = "Forritun",
                semester = "Summer 2016",
                school = "Computer Science"
            };

            var f3 = new CourseModel
            {
                Id = 23,
                name = "Matlab",
                semester = "Spring 2016",
                school = "Engineering"
            };

            var f4 = new CourseModel
            {
                Id = 24,
                name = "Gagnaskipan",
                semester = "Spring 2016",
                school = "Computer Science"
            };
            _service = new CourseService(mockDb);

        }
        [TestMethod]
        public void getCourseIDByNameTest()
        {
            // Arrange:
            const string course = "Matlab";

            // Act:
            var result = _service.getCourseIDByName(course);

            // Assert:
            Assert.AreEqual(23, result);
        }

        [TestMethod]
        public void getCourseIDByNameTest2()
        {
            // Arrange:
            const string course = "Math";

            // Act:
            var result = _service.getCourseIDByName(course);

            // Assert:
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void getCourseNameByIDTest()
        {
            // Arrange:
            const int course = 24;

            // Act:
            var result = _service.getCourseNameByID(course);

            // Assert:
            Assert.AreEqual("Gagnaskipan", result);
        }

        [TestMethod]
        public void getCourseNameByIDTest2()
        {
            // Arrange:
            const int course = 55;

            // Act:
            var result = _service.getCourseNameByID(course);

            // Assert:
            Assert.AreEqual("", result);
        }
    }
}
