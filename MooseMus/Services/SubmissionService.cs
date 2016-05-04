using MooseMus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Services
{
    public class SubmissionService
    {
        private ApplicationDbContext _db;

        public void AssignmentsService() //er ekki alveg viss hvort þetta eigi að vera void
        {
            _db = new ApplicationDbContext();
        }
        //skilar inntaki/úttaki forrits sem var sent inn
        public string getFeedback()
        {
            return null;
        }

        public void submitProjectPart(string projectPath) //við gætum gert spes ViewModel fyrir þetta, er ekki viss um að það þurfi samt. 
        {

        }

        public void uploadProjectPart(string projectPath) //tekur í raun bara inn path á það sem nemandi sendir inn? 
        {

        }
    }
}