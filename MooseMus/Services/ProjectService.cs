using System.Collections.Generic;
using MooseMus.Models.ViewModels;
using MooseMus.Models;
using MooseMus.Models.Entities;
using System;
using System.Linq;
using System.Diagnostics;

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

        public CourseModel getCourseByProjectID(int projID)
        {
            var project = _db.project.FirstOrDefault(x => x.ID == projID);
            var course = _db.course.FirstOrDefault(x => x.Id == project.courseID);
            return course;
        }

        public CourseModel getCourseByProjectPart(int proParID)
        {
            var project = _db.projectPart.FirstOrDefault(x => x.ID == proParID);
            var theProject = _db.project.FirstOrDefault(x => x.ID == project.ID);
            var course = getCourseByProjectID(theProject.ID);
            return course;
        }
        public ProjectModel getProjectByProjectPartID(int proParID)
        {
            var project = _db.projectPart.FirstOrDefault(x => x.ID == proParID);
            var theProject = _db.project.FirstOrDefault(x => x.ID == project.ID);
            return theProject;
        }

        public ProjectModel getProjectByID(int projectID)
        {
            var project = _db.project.FirstOrDefault(x => x.ID == projectID);
            return project;
        }

        public ProjectPartsViewModel getProjectPartsByID(int projID)
        {
            var project = _db.project.FirstOrDefault(x => x.ID == projID);
            var list = _db.projectPart.Where(x => x.projectID == projID).ToList();
            List<ProjectPartsListViewModel> listOfParts = new List<ProjectPartsListViewModel>();
            foreach(var par in list)
            {
                var part = new ProjectPartsListViewModel()
                {
                    projectPartID = par.ID,
                    projectPartName = par.title
                };
                listOfParts.Add(part);
            };
            var model = new ProjectPartsViewModel()
            {
                projectID = projID,
                projectName = project.title,
                parts = listOfParts
            };
            
            return model;
        }

        //Sækir öll skil nemanda í tilteknum lið
        public ProjectPartModel getProjectPartByID(int projectPartID)
        {
            var projectPart = _db.projectPart.FirstOrDefault(x => x.ID == projectPartID);
            return projectPart;
        }

        public int getProjectIDByName(string name)
        {
            var proj = _db.project.FirstOrDefault(x => x.title == name);
            return proj.ID;
        }

        public List<SubmissionViewModel> getBestSubmissionsByStudent(int stuID, int projID)
        {
            List<SubmissionViewModel> best = new List<SubmissionViewModel>();
            var parts = _db.projectPart.Where(x => x.projectID == projID).ToList();

            foreach (var sub in parts)
            {
                var submission = _db.result.FirstOrDefault(x => x.projectPartID == sub.ID && x.studentID == stuID && x.bestResult == true);
                if(submission != null)
                {
                    var model = new SubmissionViewModel()
                    {
                        studentID = submission.studentID,
                        submission = submission.ID,
                        accepted = submission.accepted,
                        best = submission.bestResult,
                        title = sub.title,
                        projParID = submission.projectPartID,
                        value = sub.value
                    };
                    best.Add(model);
                }    
            }
            return best;
        }

        public List<SubmissionViewModel> getBestSubmissionsAndNoSubByStudent(int stuID, int projID)
        {
            List<SubmissionViewModel> best = new List<SubmissionViewModel>();
            var parts = _db.projectPart.Where(x => x.projectID == projID).ToList();

            foreach (var sub in parts)
            {
                var submission = _db.result.FirstOrDefault(x => x.projectPartID == sub.ID && x.studentID == stuID && x.bestResult == true);
                if (submission != null)
                {
                    var model = new SubmissionViewModel()
                    {
                        studentID = submission.studentID,
                        submission = submission.ID,
                        accepted = submission.accepted,
                        best = submission.bestResult,
                        title = sub.title,
                        projParID = submission.projectPartID,
                        value = sub.value
                    };
                    best.Add(model);
                }
                else
                {
                    var model = new SubmissionViewModel()
                    {
                        studentID = stuID,
                        submission = 0,
                        accepted = false,
                        best = false,
                        title = sub.title,
                        projParID = sub.ID,
                        value = sub.value
                    };
                    best.Add(model);
                }
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
            var proj = _db.project.FirstOrDefault(x => x.ID == project);
            var cID = proj.courseID;
            var studentInCourse = _db.courseUser.Where(x => x.courseID == cID && x.role == "Student").Select(x => x.userID).ToList();
            List<UserViewModel> student = new List<UserViewModel>();

            foreach(var stu in studentInCourse)
            {           
                var stuInCourse = _db.user.FirstOrDefault(x => x.ID == stu);
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
            var model = _db.user.FirstOrDefault(x => x.ID == userID);
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

        //Kennari uppfærir verkefnispart í námskeiði
        public void updateProjectPart(TeacherAddProjectPartViewModel projParToEdit)
        {
            ProjectPartModel project = _db.projectPart.Where(x => x.ID == projParToEdit.ID).FirstOrDefault();
            if (project != null)
            {
                project.projectID = projParToEdit.projectID;
                project.title = projParToEdit.partName;
                project.description = projParToEdit.partDescription;
                project.input = projParToEdit.input;
                project.output = projParToEdit.output;
                project.value = projParToEdit.value;
                try
                {
                    _db.SaveChanges();
                }

                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return;
        }

        //Kennari bætir við verkefni í námskeiði
        public void addProject(TeacherAddEditViewModel projectToAdd)
        {
            ProjectModel nProject = new ProjectModel();

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

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public string getOutput(int projParID)
        {
            var output = _db.projectPart.FirstOrDefault(x => x.ID == projParID);
            return output.output;
        }

        public void addProjectPart(TeacherAddProjectPartViewModel partToAdd)
        {
            ProjectPartModel nPPart = new ProjectPartModel();

            nPPart.projectID = partToAdd.projectID;
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

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}