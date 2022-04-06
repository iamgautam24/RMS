using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class UsersRole
    {
        public int Id { get; set; }
       
        public string RoleName { get; set; }

        public bool IsSelected { get; set; }
    }
}