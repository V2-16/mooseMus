using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.ViewModels
{
    public class AddUserViewModel
    {
        
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string ssn { get; set; }
    }
}