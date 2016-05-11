using MooseMus.Models;
using MooseMus.Models.ViewModels;
using MooseMus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MooseMus.Services
{
    public class CourseService
    {
        private ApplicationDbContext _db;

        public CourseService() 
        {
            _db = new ApplicationDbContext();
        }

        public int getCourseIDByName(string courseName)
        {
            // XXX(snaedis): The page was faulting due to this, not sure how to handle
            try
            {
                var course = _db.course.FirstOrDefault(x => x.name == courseName);
                return course.Id;

            }
            catch
            {
                return 0;
            }
        }

        public string getCourseNameByID(int courseID)
        {
            var course = _db.course.FirstOrDefault(x => x.Id == courseID);
            return course.name;
        }

        public AddCourseViewModel getCourseByID(int courseID)
        {
            var course = _db.course.FirstOrDefault(x => x.Id == courseID);

            var model = new AddCourseViewModel
            {
                name = course.name,
                semester = course.semester,
                school = course.school
            };

            return model;
        }

        public int getCourseIDByCourseName(string name) //erum með tvö svona föll, þurfum að sameina. 
        {
            var course = _db.course.FirstOrDefault(x => x.name == name);
            if (course == null)
            {
                //should we implement error catch here?
                return 0;
            }
            return course.Id;
        }

        public CourseProjectsViewModel getCourseProjects(int cID)
        {
            var course = _db.course.FirstOrDefault(x => x.Id == cID);

            List<ProjectViewModel> projectNames = getProjectsByCourse(cID);

            var model = new CourseProjectsViewModel
            {
                name = course.name,
                projects = projectNames, 
            };

            return model;
        }

        public List<ProjectViewModel> getProjectsByCourse(int courseID)
        {
            var projects = _db.project.Where(x => x.courseID == courseID).ToList();
            List<ProjectViewModel> projectList = new List<ProjectViewModel> { };
            foreach (var i in projects)
            {
                var model = new ProjectViewModel()
                {
                    name = i.title,
                    projectID = i.ID
                };
                projectList.Add(model);
            };

            return projectList;
        }
        public void addCourseByID(AddCourseViewModel courseToUpdate)
        {
            CourseModel newCourse = new CourseModel();

            newCourse.name = courseToUpdate.name;
            newCourse.semester = courseToUpdate.semester;
            newCourse.school = courseToUpdate.school;

            if (courseToUpdate != null)
            {
                _db.course.Add(newCourse);
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        public void updateCourseByID(AddCourseViewModel courseToUpdate)
        {
            var id = getCourseIDByCourseName(courseToUpdate.name);
            CourseModel course = _db.course.Where(x => x.Id == id).FirstOrDefault();
            if(course != null)
            {
                course.Id = id;
                course.name = courseToUpdate.name;
                course.semester = courseToUpdate.semester;
                course.school = courseToUpdate.school;
                _db.SaveChanges();
            }
            return;
        }

     
        public void addUserToCourse(CourseUsersViewModel model)
        {
            CourseUsersModel newConnect = new CourseUsersModel();

            newConnect.userID = model.userID;
            newConnect.courseID = model.courseID;
            newConnect.role = model.role;

            if (model != null)
            {
                _db.courseUser.Add(newConnect);
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        public List<CourseModel> getAllCourses()
        {
            return _db.course.ToList();
        }
    }
}