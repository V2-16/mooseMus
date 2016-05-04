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

        public void AssignmentsService() //er ekki alveg viss hvort þetta eigi að vera void
        {
            _db = new ApplicationDbContext();
        }

        public void getCourseByID(int courseID)
        {

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