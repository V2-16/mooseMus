using MooseMus.Models;
using MooseMus.Models.Entities;
using MooseMus.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace MooseMus.Services
{
    public class SubmissionService
    {
        private ApplicationDbContext _db;

        public SubmissionService() 
        {
            _db = new ApplicationDbContext();
        }
        //skilar inntaki/úttaki forrits sem var sent inn
        public void saveResult(int stuID, int proParID, bool accepted, List<List<string>> result, List<Boolean> partsAccepted)
        {
            ResultModel nResult = new ResultModel();

            var studentOutput = "";
            foreach(var res in result)
            {
                foreach (var i in res)
                {
                    studentOutput += i;
                    studentOutput += '\n';
                }
                studentOutput += "NewPair";
            }

            var partsAccept = "";
            foreach(var res in partsAccepted)
            {
                partsAccept += res;
                partsAccept += '\n';
            }

            var best = _db.result.FirstOrDefault(x => x.bestResult == true);
            if (accepted == true)
            {
                if (best != null)
                {
                    best.bestResult = false;
                }
                nResult.bestResult = true;
            }
            else
            {
                if(best == null)
                {
                    nResult.bestResult = true;
                }
                else
                {
                    nResult.bestResult = false;
                }
            }

            nResult.studentID = stuID;
            nResult.projectPartID = proParID;
            nResult.accepted = accepted;
            nResult.partsAccepted = partsAccept;
            nResult.result = studentOutput;

            saveResults(nResult);
        }

        public void saveResults(ResultModel nResult)
        {
            if (nResult != null)
            {
                _db.result.Add(nResult);
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

        public void cleanDir(string path)
        {
            System.IO.DirectoryInfo dir = new DirectoryInfo(path);

            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }
        }

        public List<String> cleanUpInpOutp (string list)
        {
            string[] seperators = new string[] { "\r\n", "\n" };
            List<String> listToReturn = list.Split(seperators, StringSplitOptions.None).ToList();
            listToReturn.Remove("");

            return listToReturn;
        }
        public List<String> splitByRun(string str)
        {
            string[] pairSeperators = new string[] { "NewPair" };
            var res = str.Split(pairSeperators, StringSplitOptions.None).ToList();
            return res;
        }
        public StudentSubmitViewModel getSubmission(int submissionID)
        {
            var subm = _db.result.SingleOrDefault(x => x.ID == submissionID);
            var ppart = _db.projectPart.SingleOrDefault(x => x.ID == subm.projectPartID);

            var outputExp = splitByRun(ppart.output);
            var outputObt = splitByRun(subm.result);
            var partsAccepted = cleanUpInpOutp(subm.partsAccepted).ToList();
            List<OutputViewModel> results = new List<OutputViewModel>();
            for(int i = 0; i < outputExp.Count; i++)
            {
                var oneModel = new OutputViewModel();

                oneModel.outputExpected = cleanUpInpOutp(outputExp[i]);
                oneModel.outputObtained = cleanUpInpOutp(outputObt[i]);
                if(partsAccepted[i] == "True")
                {
                    oneModel.accepted = true;
                }
                else
                {
                    oneModel.accepted = false;
                }
                results.Add(oneModel);
            }
            var model = new StudentSubmitViewModel()
            {
                studentID = subm.studentID,
                projectPartID = ppart.ID,
                projectPartName = ppart.title,
                description = ppart.description,
                result = results
            };
            if (subm.accepted)
            {
                model.projectAccepted = "Accepted!";
            }
            else
            {
                model.projectAccepted = "not Accepted!";
            }
            return model;
        }

        public ProcessStartInfo processStart(string exeFilePath)
        {
            var processInfoExe = new ProcessStartInfo(exeFilePath, "");
            processInfoExe.UseShellExecute = false;
            processInfoExe.RedirectStandardInput = true;
            processInfoExe.RedirectStandardOutput = true;
            processInfoExe.RedirectStandardError = true;
            processInfoExe.CreateNoWindow = true;

            return processInfoExe;
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