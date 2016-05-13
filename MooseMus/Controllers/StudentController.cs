﻿using MooseMus.Models.ViewModels;
using MooseMus.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using MooseMus.Handlers;

namespace MooseMus.Controllers
{
    [CustomHandleError]
    public class StudentController : Controller
    {
        private CourseService _cservice = new CourseService();
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

        //Nemandi fer í skilasvæði
        public ActionResult submitAProjectPart(int stuID, int proParID)
        {
            var proPar = _pservice.getProjectPartByID(proParID);
            var model = new StudentSubmitViewModel()
            {
                studentID = stuID,
                projectPartID = proPar.ID,
                projectPartName = proPar.title,
                description = proPar.description,
                accepted = false
            };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult submitAProjectPart(StudentSubmitViewModel data)
        {
            // Read the file and display it line by line.
            // Set up our working folder, and the file names/paths.
            // In this example, this is all hardcoded, but in a
            // real life scenario, there should probably be individual
            // folders for each user/assignment/milestone.
            var projectAccepted = data.accepted;
            var workingFolder = "C:\\Temp\\Mooshak2Code\\";
            var cppFileName = data.projectPartID + ".cpp";
            var exeFilePath = workingFolder + data.projectPartID + ".exe";
            data.fileUploaded.SaveAs(workingFolder + cppFileName);
            
            var compilerFolder = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\VC\\bin\\";
            // Execute the compiler:
            compile(workingFolder, compilerFolder, cppFileName);

            string[] seperators = new string[] { "\r\n", "\n" };
            var outputFromTeacher = _pservice.getOutput(data.projectPartID).Split(seperators, StringSplitOptions.None).ToList();

            var inputFromTeacher = _pservice.getInput(data.projectPartID).Split(seperators, StringSplitOptions.None).ToList();

            // Check if the compile succeeded, and if it did,
            // we try to execute the code:
            if (System.IO.File.Exists(exeFilePath))
            {
                var processInfoExe = new ProcessStartInfo(exeFilePath, "");
                processInfoExe.UseShellExecute = false;
                processInfoExe.RedirectStandardInput = true;
                processInfoExe.RedirectStandardOutput = true;
                processInfoExe.RedirectStandardError = true;
                processInfoExe.CreateNoWindow = true;
                using (var processExe = new Process())
                {
                    processExe.StartInfo = processInfoExe;
                    processExe.Start();

                    foreach(var inp in inputFromTeacher)
                    {
                        processExe.StandardInput.WriteLine(inp);
                    }

                    // We then read the output of the program:
                    var lines = new List<string>();
                    while (!processExe.StandardOutput.EndOfStream)
                    {
                        lines.Add(processExe.StandardOutput.ReadLine());
                    }

                    projectAccepted = outputFromTeacher.SequenceEqual(lines);
                    _sservice.saveResult(data.studentID, data.projectPartID, projectAccepted, lines); //Saving the data to database
                    ViewBag.Output = lines;
                }
            }
            else
            {
                ViewBag.Output = "Uh Oh! Your program did not compile, go back and fix it please..";
            }

            List<String> outputExpected = new List<String>();
            for (int i = 0; i < outputFromTeacher.Count; i++)
            {
                outputExpected.Add(outputFromTeacher[i]);
            }

            _sservice.cleanDir(workingFolder);
            ViewBag.Success = true;
            var model = new StudentSubmitViewModel()
            {
                studentID = data.studentID,
                projectPartID = data.projectPartID,
                projectPartName = data.projectPartName,
                description = data.description,
                accepted = projectAccepted,
                output = outputExpected
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
    }

}

