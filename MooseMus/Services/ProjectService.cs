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
            var proj = _db.project.SingleOrDefault(x => x.title == name);
            return proj.ID;
        }

        public List<SubmissionViewModel> getBestSubmissionsByStudent(int studentID, string projectName)
        {
            var projID = getProjectIDByName(projectName);
            List<SubmissionViewModel> best = new List<SubmissionViewModel>();
            var submissions = _db.projectPart.Where(x => x.projectID == projID).ToList();

            foreach (var sub in submissions)
            {
                var submission = _db.result.SingleOrDefault(x => x.projectPartID == sub.ID && x.bestResult == true);
                var model = new SubmissionViewModel()
                {
                    studentID = submission.studentID,
                    submission = submission.ID,
                    accepted = submission.accepted,
                    best = submission.accepted
                };
                best.Add(model);
            }
            return best;
        }
        /****************** KENNARI **************************/
        //Skilar öllum nemendum sem hafa skilað tilteknu verkefni
        public List<UserViewModel> getStudentsInProject(string project)
        {
            var proj = _db.project.SingleOrDefault(x => x.title == project);
            var studentInCourse = _db.courseStudent.Where(x => x.courseID == proj.courseID).Select(x => x.studentID).ToList();
            List<UserViewModel> student = new List<UserViewModel>();

            foreach(var stu in studentInCourse)
            {           
                var stuInCourse = _db.user.SingleOrDefault(x => x.ID == stu);
                var model = new UserViewModel()
                {
                    ssn = stuInCourse.ssn,
                    name = stuInCourse.name,
                    email = stuInCourse.email,
                    userID = stuInCourse.ID
                };
                student.Add(model);
            }
            return student;
        }

        public string getUserByID(int userID)
        {
            var model = _db.user.SingleOrDefault(x => x.ID == userID);
            return model.name;
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

            if (projectToAdd != null)
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
        public void addProjectPart(TeacherAddProjectPartViewModel partToAdd)
        {
            ProjectPartModel nPPart = new ProjectPartModel();
            int? proParID = _db.project.Max(m => (int?)m.ID) + 1;
            var proID = _db.project.SingleOrDefault(m => m.ID == partToAdd.projectName);

            nPPart.ID = proParID.Value;
            nPPart.projectID = proID.ID;
            nPPart.title = partToAdd.partName;
            nPPart.description = partToAdd.partDescription;
            nPPart.input = partToAdd.input;
            nPPart.output = partToAdd.output;
            nPPart.value = partToAdd.value;

            if (partToAdd != null)
            {
                _db.projectPart.Add(nPPart);
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