using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class DbContextRMS : DbContext
    {
        public DbContextRMS() : base("Database")
        {

        }
        public DbSet<Registration> Registrations { get; set; }

    }
}