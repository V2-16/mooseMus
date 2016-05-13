using MooseMus.Models;
using MooseMus.Models.Entities;
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
    }
}