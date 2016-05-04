using MooseMus.Models;
using MooseMus.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooseMus.Services
{
    public class UserService
    {
        private ApplicationDbContext _db;

        public void AssignmentsService() //er ekki alveg viss hvort þetta eigi að vera void
        {
            _db = new ApplicationDbContext();
        }
        public UserHomeViewModel getUserByID(int userID)
        {
            return null;
        }

        public void addUserByID(AddUserViewModel newUser)
        {

        }

        public void updateUserByID(AddUserViewModel editUser)
        {

        }

        public int getUserIDByUserName(string username)
        {
            return 0;
        }

        public string getPasswordByID(int userID)
        {
            return null;
        }

        //Sækir lista af námskeiðum sem notandi er skráður í (sem nemandi eða kennari)
        public UserViewModel getCoursesByUser(int userID)
        {
            return null;
        }
    }
}