using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.Entities
{
    public class ProjectModel
    {
        public int projectID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime deadline { get; set; }
        public int courseID { get; set; }
    }
}