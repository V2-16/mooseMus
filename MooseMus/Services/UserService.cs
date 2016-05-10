using MooseMus.Models;
using MooseMus.Models.Entities;
using MooseMus.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            UserModel nUser = new UserModel();
            
            nUser.name = newUser.name;
            nUser.email = newUser.email;
            nUser.password = newUser.password;
            nUser.ssn = newUser.ssn;

            if(newUser != null)
            {
                _db.user.Add(nUser);
            }

            try
            {
                _db.SaveChanges();
            }

            catch (Exception e)
            {
              
            }
        }

        public void updateUserByID(AddUserViewModel editUser)
        {
            var id = getUserIDByUserSSN(editUser.ssn);
            UserModel user = _db.user.Where(x => x.ID == id).SingleOrDefault();

            if (user != null)
            {
                user.ID = id;
                user.name = editUser.name;
                user.password = editUser.password;
                user.ssn = editUser.ssn;
                _db.SaveChanges();
            }
            return;
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

        public int getUserIDByUserSSN(string ssn)
        {
            var user = _db.user.SingleOrDefault(x => x.ssn == ssn);
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

        public int getUserIDByPasswordAndConfirm(string password, int userID)
        {
            var user = _db.user.SingleOrDefault(x => x.password == password && x.ID == userID);
            if (user == null)
            {
                return 0;
            }
            return user.ID;
        }

        public AddUserViewModel getAddUserViewModelByID(int id)
        {
            var user = _db.user.SingleOrDefault(x => x.ID == id);

            var model = new AddUserViewModel
            {
                name = user.name,
                ssn = user.ssn,
                email = user.email,
                password = user.password
            };
            return model;
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

        public EnrolledCourseModel getUserByCourse(int courseID)
        {
            var courseUsers = _db.courseUser.Where(x => x.courseID == courseID).ToList();
            var usersIds = courseUsers.Select(x => x.userID);
            var usersNotIn = _db.user.Where(x => !usersIds.Contains(x.ID)).ToList();
            var usersIn = _db.user.Where(x => usersIds.Contains(x.ID)).ToList();

            EnrolledCourseModel model = new EnrolledCourseModel { enrolledUsers = usersIn, unEnrolledUsers = usersNotIn };
            
            return model;
                
            
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

        public List<UserModel> getAllUsers()
        {
            return _db.user.ToList();
        }
    }
}