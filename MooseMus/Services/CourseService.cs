using MooseMus.Models;
using MooseMus.Models.ViewModels;
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
            var courseNa = courseName;
            var course = _db.course.SingleOrDefault(x => x.name == courseName);
            return course.Id;
        }

        public CourseProjectsViewModel getCourseProjects(int courseID)
        {
            var course = _db.course.SingleOrDefault(x => x.Id == courseID);

            List<ProjectViewModel> projectNames = getProjectsByCourse(courseID);

            var model = new CourseProjectsViewModel
            {
                name = course.name,
                projects = projectNames 
            };

            return model;
        }

        public List<ProjectViewModel> getProjectsByCourse(int courseID)
        {
            var projects = _db.project.Where(x => x.courseID == courseID).ToList();
            List<ProjectViewModel> projectNames = new List<ProjectViewModel> { };
            foreach (var i in projects)
            {
                projectNames.Add(new ProjectViewModel { name = i.title });
            };

            return projectNames;
        }
        public void addCourseByID(AddCourseViewModel courseToUpdate)
        {

        }

        public void updateCourseByID(AddCourseViewModel courseToUpdate)
        {

        }

        public void addStudentToCourse(int userID, int courseID)
        {

        }

        public void addTeacherToCourse(int userID, int courseID)
        {

        }
    }
}