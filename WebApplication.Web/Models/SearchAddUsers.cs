using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class SearchAddUsers
    {
        public GenericSearch<User> userSearch { get; set; } = new GenericSearch<User>();
        public string newUser = "";
        public List<User> players { get; set; } = new List<User>();
    }
}
