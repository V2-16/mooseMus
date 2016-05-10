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

        public ProjectModel getProjectByID(int projectID)
        {
            var project = _db.project.SingleOrDefault(x => x.ID == projectID);
            return project;
        }

        //Sækir öll skil nemanda í tilteknum lið
        public ProjectPartModel getProjectPartByID(int projectPartID)
        {
            var projectPart = _db.projectPart.SingleOrDefault(x => x.ID == projectPartID);
            return projectPart;
        }

        public int getProjectIDByName(string name)
        {
            var proj = _db.project.SingleOrDefault(x => x.title == name);
            return proj.ID;
        }

        public List<SubmissionViewModel> getBestSubmissionsByStudent(int studentID, int projID)
        {
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
                    best = submission.bestResult,
                    title = sub.title,
                    projParID = submission.projectPartID
                };
                best.Add(model);
            }
            return best;
        }

        public List<SubmissionViewModel> getSubmissionsByStudentAndPart(int stuID, int proPartID)
        {
            List<SubmissionViewModel> best = new List<SubmissionViewModel>();
            var submissions = _db.result.Where(x => x.projectPartID == proPartID && x.studentID == stuID).ToList();

            foreach (var sub in submissions)
            {
                var model = new SubmissionViewModel()
                {
                    studentID = stuID,
                    submission = sub.ID,
                    accepted = sub.accepted,
                    best = sub.bestResult
                };
                best.Add(model);
            }
            return best;
        }
        /****************** KENNARI **************************/
        //Skilar öllum nemendum sem hafa skilað tilteknu verkefni
        public List<UserViewModel> getStudentsInProject(int project)
        {
            var proj = _db.project.SingleOrDefault(x => x.ID == project);
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