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
        private CourseService _courseService = new CourseService();
        private readonly IAppDataContext _db;

        public UserService(IAppDataContext dbContext) 
        {
            _db = dbContext ?? new ApplicationDbContext();
        }
        
        //Returns username and the courses associated to him/her using the user id
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

        //Adding user to DB 
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

        //Updating user info in DB
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

        //Returns userID using users name
        public int getUserIDByUserName(string name)
        {
            var user = _db.user.SingleOrDefault(x => x.name == name);
            if(user == null)
            {
                return 0;
            }
            return user.ID;
        }

        //Returns userID byr users SSN
        public int getUserIDByUserSSN(string ssn)
        {
            var user = _db.user.SingleOrDefault(x => x.ssn == ssn);
            if(user == null)
            {
                return 0;
            }
            return user.ID;
        }

        //Returns userId using users password
        public int getUserIDByPassword(string password)
        {
           var user = _db.user.SingleOrDefault(x => x.password == password);
           if (user == null)
           {
                return 0;
           }
           return user.ID;
        }

        //Confirming userId and password match
        public int getUserIDByPasswordAndConfirm(string password, int userID)
        {
            var user = _db.user.SingleOrDefault(x => x.password == password && x.ID == userID);
            if (user == null)
            {
                return 0;
            }
            return user.ID;
        }

        //Get userViewModel
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

        //Get users password by userID
        public string getPasswordByID(int userID)
        {
            var user = _db.user.SingleOrDefault(x => x.ID == userID);
            if(user == null)
            {
                return "0";
            }
            return user.password;
        }

        //Returns a list that user is associated with
        public List<CourseViewModel> getCoursesByUser(int user)
        {
            var courses = _db.courseUser.Where(x => x.userID == user).ToList();
            List<CourseViewModel> courseNames = new List<CourseViewModel> { };
            foreach (var i in courses)
            {
                var course = _db.course.SingleOrDefault(x => x.Id == i.courseID);
                var listOfCourses = new CourseViewModel
                {
                    name = course.name,
                    role = i.role
                };
                courseNames.Add(listOfCourses);
            };
            return courseNames;
        }

        //returns a list of users(model entity) that are associated with given course
        public List<UserModel> getUsersByCourse(int courseID)
        {
            var courseUsers = _db.courseUser.Where(x => x.courseID == courseID).ToList();
            List<UserModel> usersIn = new List<UserModel> { };
            foreach (var i in courseUsers)
            {
                var user = _db.user.SingleOrDefault(x => x.ID == i.userID);
                usersIn.Add(user);
            };
            return usersIn;            
        }
        
        //Return a list of teachers(user model-entities) teachers accociated with given course
        public List<UserModel> getTeachersAssociated(List<UserModel> usersInCourse, int courseID)
        {
            List<UserModel> teachersIn = new List<UserModel> { };
            var courseName = _courseService.getCourseNameByID(courseID);
            foreach (var i in usersInCourse)
            {
                if (teacherOrStudent(i.ID, courseName) == "Teacher   ")
                {
                    teachersIn.Add(i);
                }
            }
            return teachersIn;
        }
        
        //Returns a list of students assoceiated with a given course
        public List<UserModel> getStudentsAssociated(List<UserModel> usersIn, List<UserModel> teachers)
        {
            List<UserModel> studentsIn = usersIn.Except(teachers).ToList();
            return studentsIn;  
        }

        //Returns a list of all users except the users in given course
        public List<UserModel> getUsersNotAssociated(List<UserModel> users)
        {
            var allUsers = getAllUsers();
            var admin = _db.user.Where(x => x.name == "Admin");
            List<UserModel> usersNotIn = allUsers.Except(users).Except(admin).ToList();
            return usersNotIn;
        }

        //Returns a viewmodel containing the lists of associated(non associated) users
        public CourseUsersViewModel usersAssociated(CourseUsersViewModel courseModel)
        {
            var usersIn = getUsersByCourse(courseModel.courseID);
            var teachersIn = getTeachersAssociated(usersIn, courseModel.courseID);
            var studentsIn = getStudentsAssociated(usersIn, teachersIn);
            var usersNotIn = getUsersNotAssociated(usersIn);

            CourseUsersViewModel model = new CourseUsersViewModel
            {
                teachers = teachersIn,
                students = studentsIn,
                unEnrolledUsers = usersNotIn
            };
            return model;
        }

        //Checks if user in given course is a teacher or studenf
        public string teacherOrStudent(int userID, string course)
        {
            var theCourse = _db.course.SingleOrDefault(x => x.name == course);
            var user = _db.courseUser.SingleOrDefault(x => x.userID == userID && x.courseID == theCourse.Id);
            return user.role;
        }

        //returns a list of all users in the database
        public List<UserModel> getAllUsers()
        {
            return _db.user.ToList();
        }
    }
}