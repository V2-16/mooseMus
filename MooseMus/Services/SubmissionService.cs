﻿using MooseMus.Models;
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
        // Saves optained input/output from students submission
        public void saveResult(int stuID, int proParID, bool accepted, List<List<string>> result)
        {
            ResultModel nResult = new ResultModel();

            nResult.studentID = stuID;
            nResult.projectPartID = proParID;
            nResult.accepted = accepted;
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
            nResult.result = studentOutput;

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
    }
}