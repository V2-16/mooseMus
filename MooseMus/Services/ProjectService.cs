using System.Collections.Generic;
using MooseMus.Models.ViewModels;
using MooseMus.Models;

namespace MooseMus.Services
{
    public class ProjectService
    {
        private ApplicationDbContext _db;

        public void AssignmentsService() //er ekki alveg viss hvort þetta eigi að vera void
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
            //TODO;
        }
    }
}