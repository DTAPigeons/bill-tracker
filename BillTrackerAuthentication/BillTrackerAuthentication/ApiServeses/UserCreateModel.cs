using BillTrackerAuthentication.Quickstart.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillTrackerAuthentication.ApiServeses
{
    public class UserCreateModel
    {
        public string AccountName { get; set; }
        public string Name { get; set; }
        public double Income { get; set; }

        public double TotalSavings { get; set; }

        public UserCreateModel(UserRegisterViewModel user)
        {
            AccountName = user.RegisterInputModel.Email;
            Name = user.FirstName + " " + user.LastName;
            Income = user.Income;
            TotalSavings = user.Income;
        }
    }
}
