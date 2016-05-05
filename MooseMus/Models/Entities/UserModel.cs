using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Models.Entities
{
    public class UserModel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}