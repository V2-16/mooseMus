using System.Collections.Generic;
using MooseMus.Models.ViewModels;
using MooseMus.Models;
using MooseMus.Models.Entities;
using System;
using System.Linq;

namespace MooseMus.Services
{
    public class ProjectService
    {
        private ApplicationDbContext _db;

        public ProjectService() 
        {
            _db = new ApplicationDbContext();
        }
        /****************** KENNARI & NEMANDI **************************/
        //Skilar lista af öllum verkefnum í námskeið.
        public List<CourseViewModel> getProjectsByCourseID(int courseID)
        {
            //TODOO;
            return null;
        }

        //Skilar lista af öllum liðum í verkefni
        public List<ProjectViewModel> getProjectByID(int projectID)
        {
            //TODO;
            return null;
        }

        //Sækir öll skil nemanda í tilteknum lið
        public StudentProjectPartViewModel getProjectPartByStudent(int studentID, int projectPartID)
        {
            //TODO;
            return null;
        }

        public int getProjectIDByName(string name)
        {

            return 9;
        }
        /****************** KENNARI **************************/
        //Skilar öllum nemendum sem hafa skilað tilteknu verkefni
        public TeacherProjectViewModel getStudentsInProject(int projectID)
        {
            //TODO;
            return null;
        }

        //Sækir bestu skil í hverjum lið hjá nemanda
        public TeacherProjectStudentViewModel getProjectByStudent(int studentID, int projectID)
        {
            //TODO;
            return null;
        }

        //Sækir lið í verkefni fyrir kennara til þess að breyta (uppfærði nafn úr getProjectPartByID)
        public TeacherAddEditViewModel getProjectPartByIDToEdit(int projectPartID)
        {
            //TODO;
            return null;
        }

        //Kennari uppfærir verkefni í námskeiði
        public void updateProject(TeacherAddEditViewModel projectToEdit)
        {
            //TODO;
        }

        //Kennari bætir við verkefni í námskeiði
        public void addProject(TeacherAddEditViewModel projectToAdd)
        {
            ProjectModel nProject = new ProjectModel();
            int? proID = _db.project.Max(m => (int?)m.ID) + 1;
            if(proID == null)
            {
                proID = 1;
            }
            nProject.ID = proID.Value;
            nProject.title = projectToAdd.title;
            nProject.description = projectToAdd.projectDescription;
            nProject.courseID = projectToAdd.courseID;
            nProject.deadline = DateTime.Today;

            if (nProject != null)
            {
                _db.project.Add(nProject);
            }

            try
            {
                _db.SaveChanges();
            }

            catch (Exception e)
            {

            }
        }
    }
}