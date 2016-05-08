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

        public void getCourseByID(int courseID)
        {

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

        }

        public void addStudentToCourse(int userID, int courseID)
        {

        }

        public void addTeacherToCourse(int userID, int courseID)
        {

        }
    }
}