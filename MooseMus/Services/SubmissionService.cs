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
        public void saveResult(int stuID, int proParID, bool accepted, List<string> result)
        {
            ResultModel nResult = new ResultModel();

            nResult.studentID = stuID;
            nResult.projectPartID = proParID;
            nResult.accepted = accepted;
            nResult.result = result.ToString();

            if(accepted == true)
            {
                var best = _db.result.FirstOrDefault(x => x.bestResult == true);
                if (best != null)
                {
                    best.bestResult = false;
                }
                nResult.bestResult = true;
            }
            else
            {
                nResult.bestResult = false;
            }

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
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }
}