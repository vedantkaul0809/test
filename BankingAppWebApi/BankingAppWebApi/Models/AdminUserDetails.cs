using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingAppWebApi.Models
{
    public class AdminUserDetails
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string MobileNumber
        {
            get;
            set;
        }
    }
}