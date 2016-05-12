using MooseMus.Models.ViewModels;
using MooseMus.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace MooseMus.Controllers
{
    public class StudentController : Controller
    {
        private CourseService _cservice = new CourseService();
        private ProjectService _pservice = new ProjectService();
        private UserService _uservice = new UserService(null);

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
                description = proPar.description
            };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult submitAProjectPart(StudentSubmitViewModel data)
        {

            // To simplify matters, we declare the code here.
            // The code would of course come from the student!
           /* var code = "#include <iostream>\n" +
            "using namespace std;\n" +
            "int main()\n" +
            "{\n" +
            "cout << \"Hello world\" << endl;\n" +
            "cout << \"The output should contain two lines\" << endl;\n" +
            "return 0;\n" +
            "}";*/

            // Read the file and display it line by line.
            // Set up our working folder, and the file names/paths.
            // In this example, this is all hardcoded, but in a
            // real life scenario, there should probably be individual
            // folders for each user/assignment/milestone.
            var workingFolder = "C:\\Temp\\Mooshak2Code\\";
            var cppFileName = "Hello2.cpp";
            var exeFilePath = workingFolder + "Hello2.exe";
            data.fileUploaded.SaveAs(workingFolder + cppFileName);
            // Write the code to a file, such that the compiler
            // can find it:
           // System.IO.File.WriteAllText(workingFolder + cppFileName, code);

            // In this case, we use the C++ compiler (cl.exe) which ships
            // with Visual Studio. It is located in this folder:
            var compilerFolder = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\VC\\bin\\";
            // There is a bit more to executing the compiler than
            // just calling cl.exe. In order for it to be able to know
            // where to find #include-d files (such as <iostream>),
            // we need to add certain folders to the PATH.
            // There is a .bat file which does that, and it is
            // located in the same folder as cl.exe, so we need to execute
            // that .bat file first.

            // Using this approach means that:
            // * the computer running our web application must have
            //   Visual Studio installed. This is an assumption we can
            //   make in this project.
            // * Hardcoding the path to the compiler is not an optimal
            //   solution. A better approach is to store the path in
            //   web.config, and access that value using ConfigurationManager.AppSettings.

            // Execute the compiler:
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




                    string[] seperators = new string[] { "\r\n", "\n" };
                    var inputFromTeacher = _pservice.getInput(data.projectPartID).Split(seperators, StringSplitOptions.None).ToList();
                    var realInput = inputFromTeacher[0];
                    processExe.StandardInput.WriteLine(realInput);

                    
                    // In this example, we don't try to pass any input
                    // to the program, but that is of course also
                    // necessary. We would do that here, using
                    // processExe.StandardInput.WriteLine(), similar
                    // to above.

                    // We then read the output of the program:
                    var lines = new List<string>();
                    while (!processExe.StandardOutput.EndOfStream)
                    {
                        lines.Add(processExe.StandardOutput.ReadLine());
                    }
              
                    var outputFromTeacher = _pservice.getOutput(data.projectPartID).Split(seperators, StringSplitOptions.None).ToList();
                    lines.Add("***************RESULT***************");
                    if (outputFromTeacher.SequenceEqual(lines))
                    {
                        lines.Add("Success! Your submission has been accepted");
                    }
                    else
                    {
                        lines.Add("Your submission has not been accepted");
                        lines.Add("Your output should be:");
                        for(int i = 0; i < outputFromTeacher.Count; i++)
                        {
                            lines.Add(outputFromTeacher[i]);
                        }
                    }
                    ViewBag.Output = lines;
                }
            }

            // TODO: We might want to clean up after the process, there
            // may be files we should delete etc.
            ViewBag.Success = true;
            var model = new StudentSubmitViewModel()
            {
                studentID = data.studentID,
                projectPartID = data.projectPartID,
                projectPartName = data.projectPartName,
                description = data.description
            };
            return View(model);
        }
        public ActionResult uploadProjectPart()
        {
            return View();
        }

        [HttpGet]
        public ActionResult submission()
        {
            return View();
        }

        [HttpPost]
        public ActionResult submission(StudentSubmitViewModel model)
        {
            return View();
        }
    }


}

