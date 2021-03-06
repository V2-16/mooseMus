﻿using MooseMus.Models.ViewModels;
using MooseMus.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using MooseMus.Handlers;

namespace MooseMus.Controllers
{
    [CustomHandleError]
    public class StudentController : Controller
    {
        // Initializing services  
        private CourseService _cservice = new CourseService(null);
        private ProjectService _pservice = new ProjectService();
        private UserService _uservice = new UserService(null);
        private SubmissionService _sservice = new SubmissionService();

        // GET: Student
        public ActionResult Index(string course, int stuID)
        {
            var courseID = _cservice.getCourseIDByName(course);
            var model = _cservice.getCourseProjects(courseID);
            model.studentID = stuID;
            return View(model);
        }

        // View of projectpart depending on given student and project
        public ActionResult viewProject(int stuID, int projID)
        {
            var student = _uservice.getUserByID(stuID);
            var project = _pservice.getProjectByID(projID);
            List<SubmissionViewModel> part = _pservice.getBestSubmissionsAndNoSubByStudent(stuID, projID);
            var model = new TeacherProjectStudentViewModel()
            {
                studentName = student.name,
                projectName = project.title,
                parts = part
            };
            return PartialView(model);
        }

        // View projectpart to submit depending on given student and project
        public ActionResult viewProjectToSubmit(int stuID, int projID)
        {
            var student = _uservice.getUserByID(stuID);
            var project = _pservice.getProjectByID(projID);
            List<SubmissionViewModel> part = _pservice.getBestSubmissionsAndNoSubByStudent(stuID, projID);
            var model = new TeacherProjectStudentViewModel()
            {
                studentName = student.name,
                projectName = project.title,
                parts = part,
                description = project.description
            };
            return PartialView(model);
        }

        // Overview of projects
        public ActionResult submitAProjectPart(int stuID, int proParID)
        {
            var proPar = _pservice.getProjectPartByID(proParID);
            var model = new StudentSubmitViewModel()
            {
                studentID = stuID,
                projectPartID = proPar.ID,
                projectPartName = proPar.title,
                description = proPar.description,
            };
            return PartialView(model);
        }

        // Given code from teacher with alterations
        [HttpPost]
        public ActionResult submitAProjectPart(StudentSubmitViewModel data)
        {
            // Read the file and display it line by line.
            // Set up our working folder, and the file names/paths.
            // In this example, this is all hardcoded, but in a
            // real life scenario, there should probably be individual
            // folders for each user/assignment/milestone.
            var projectAccepted = false;
            var workingFolder = "C:\\Temp\\Mooshak2Code\\";
            var cppFileName = data.projectPartID + ".cpp";
            var exeFilePath = workingFolder + data.projectPartID + ".exe";
            System.IO.Directory.CreateDirectory(workingFolder);
            data.fileUploaded.SaveAs(workingFolder + cppFileName);

            var compilerFolder = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\VC\\bin\\";
            // Execute the compiler:

            _sservice.compile(workingFolder, compilerFolder, cppFileName);

            var outputFromTeacherPair = _sservice.splitByRun(_pservice.getOutput(data.projectPartID));
            var inputFromTeacherPair = _sservice.splitByRun(_pservice.getInput(data.projectPartID));

            List<List<String>> outputTeacher = new List<List<String>>();
            List<List<String>> outputStudent = new List<List<String>>();
            List<bool> accepted = new List<Boolean>();
            List<OutputViewModel> results = new List<OutputViewModel>();
            bool allAccepted = true;
            string success = "Accepted!";

            // Check if the compile succeeded, and if it did,
            // we try to execute the code:
            if (System.IO.File.Exists(exeFilePath))
            {
                for (int i = 0; i < outputFromTeacherPair.Count; i++)
                {
                    var outputFromTeacher = _sservice.cleanUpInpOutp(outputFromTeacherPair[i]);
                    var inputFromTeacher = _sservice.cleanUpInpOutp(inputFromTeacherPair[i]);
                    List<String> outTeacher = new List<String>();
                    outputTeacher.Add(outputFromTeacher);

                    var processInfoExe = _sservice.processStart(exeFilePath);

                    using (var processExe = new Process())
                    {
                        processExe.StartInfo = processInfoExe;
                        processExe.Start();
                        //Incase the process is stuck in a loop or any other problem that may cause an abnormal delay
                        processExe.WaitForExit(10000);
                        foreach (var inp in inputFromTeacher)
                        {
                            processExe.StandardInput.WriteLine(inp);
                        }

                        var templines = new List<string>();

                        // We then read the output of the program:
                        while (!processExe.StandardOutput.EndOfStream)
                        {
                            templines.Add(processExe.StandardOutput.ReadLine());
                        }

                        projectAccepted = outputFromTeacher.SequenceEqual(templines);
                        outputStudent.Add(templines);
                        accepted.Add(projectAccepted);
                        var oneModel = new OutputViewModel()
                        {
                            outputObtained = templines,
                            outputExpected = outputFromTeacher,
                            accepted = projectAccepted
                        };
                        results.Add(oneModel);
                    }
                }
                if (accepted.Contains(false))
                {
                    success = "NOT accepted!";
                    allAccepted = false;
                }
                //Saving the data to database
                _sservice.saveResult(data.studentID, data.projectPartID, allAccepted, outputStudent, accepted);
            }
            else
            {
                success = "Your program did not compile, go back and fix it please..";
            }

            _sservice.cleanDir(workingFolder);
            ViewBag.Success = true;
            var model = new StudentSubmitViewModel()
            {
                studentID = data.studentID,
                projectPartID = data.projectPartID,
                projectPartName = data.projectPartName,
                description = data.description,
                result = results,
                projectAccepted = success
            };
            return View(model);
        }

        public void compile(string workingFolder, string compilerFolder, string cppFileName)
        {
            Process compiler = new Process();
            compiler.StartInfo.FileName = "cmd.exe";
            compiler.StartInfo.WorkingDirectory = workingFolder;
            compiler.StartInfo.RedirectStandardInput = true;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.StartInfo.UseShellExecute = false;

            compiler.Start();
            compiler.StandardInput.WriteLine("\"" + compilerFolder + "vcvars32.bat" + "\"");
            compiler.StandardInput.WriteLine("cl.exe /nologo /EHsc " + cppFileName);
            compiler.StandardInput.WriteLine("exit");
            string output = compiler.StandardOutput.ReadToEnd();
            compiler.WaitForExit();
            compiler.Close();
        }
        
        public ActionResult getSubmission(int submissionID)
        {
            var model = _sservice.getSubmission(submissionID);
            return PartialView("Result", model);
        }
    }
}

