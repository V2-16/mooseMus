using MooseMus.Models;
using MooseMus.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Services
{
    public class UserService
    {
        private ApplicationDbContext _db;

        public UserService() 
        {
            _db = new ApplicationDbContext();
        }
        public UserHomeViewModel getUserByID(int userID)
        {
            var user = _db.user.SingleOrDefault(x => x.ID == userID);
            List<CourseViewModel> courseNames = getCoursesByUser(userID);
            
            var model = new UserHomeViewModel
            {
                userID = user.ID,
                name = user.name,
                email = user.email,
                courses = courseNames
            };

            return model;
        }

        public void addUserByID(AddUserViewModel newUser)
        {

        }

        public void updateUserByID(AddUserViewModel editUser)
        {

        }

        public int getUserIDByUserName(string name)
        {
            var user = _db.user.SingleOrDefault(x => x.name == name);
            if(user == null)
            {
                return 0;
            }
            return user.ID;
        }

        public int getUserIDByPassword(string password)
        {
           var user = _db.user.SingleOrDefault(x => x.password == password);
           if (user == null)
           {
                return 0;
           }
           return user.ID;
        }

        public string getPasswordByID(int userID)
        {
            var user = _db.user.SingleOrDefault(x => x.ID == userID);
            if(user == null)
            {
                return "0";
            }
            return user.password;
        }

        //Sækir lista af námskeiðum sem notandi er skráður í (sem nemandi eða kennari)
        public List<CourseViewModel> getCoursesByUser(int userID)
        {
            var coursesS = _db.courseStudent.Where(x => x.studentID == userID).ToList();
            var coursesT = _db.courseTeacher.Where(x => x.teacherID == userID).ToList();
            List<CourseViewModel> courseNames = new List<CourseViewModel> { };
            foreach (var i in coursesS)
            {
                var course = _db.course.SingleOrDefault(x => x.Id == i.ID);
                courseNames.Add(new CourseViewModel { name = course.name });
            };
            foreach (var i in coursesT)
            {
                var course = _db.course.SingleOrDefault(x => x.Id == i.ID);
                courseNames.Add(new CourseViewModel { name = course.name });
            };
            return courseNames;
        }

        public string teacherOrStudent(int userID, string course)
        {
            var theCourse = _db.course.SingleOrDefault(x => x.name == course);

            var user = _db.courseStudent.SingleOrDefault(x => x.studentID == userID && x.courseID == theCourse.Id);
            if(user == null)
            {
                return "teacher";
            }
            return "student";
        }
    }
}