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
            var courses = _db.courseStudent.Where(x => x.studentID == userID).ToList();
            List<CourseViewModel> courseNames = new List<CourseViewModel> { };
            foreach(var i in courses)
            {
                var course = _db.course.SingleOrDefault(x => x.Id == i.ID);
                courseNames.Add(new CourseViewModel { name = course.name });
            };

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
        public UserViewModel getCoursesByUser(int userID)
        {
            return null;
        }
    }
}