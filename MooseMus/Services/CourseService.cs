using MooseMus.Models;
using MooseMus.Models.ViewModels;
using MooseMus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MooseMus.Models.Entities;


namespace MooseMus.Services
{
    public class CourseService
    {
        private ApplicationDbContext _db;

        public CourseService() 
        {
            _db = new ApplicationDbContext();
        }

        public AddCourseViewModel getCourseByID(int courseID)
        {
            var course = _db.course.SingleOrDefault(x => x.Id == courseID);

            var model = new AddCourseViewModel
            {
                name = course.name,
                semester = course.semester,
                school = course.school
            };

            return model;
        }

        public int getCourseIDByCourseName(string name)
        {
            var course = _db.course.SingleOrDefault(x => x.name == name);
            if (course == null)
            {
                //should we implement error catch here?
                return 0;
            }
            return course.Id;
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
            CourseModel course = _db.course.Where(x => x.Id == id).SingleOrDefault();
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

        public void addStudentToCourse(int userID, int courseID)
        {

        }

        public void addTeacherToCourse(int userID, int courseID)
        {

        }
    }
}