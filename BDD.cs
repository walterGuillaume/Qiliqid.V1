using System;
using System.Data.Entity;
using System.Linq;
using Qiliqid.V1.Models;

namespace Qiliqid.V1
{
    public class BDD : DbContext
    {
        public DbSet<Article> Articles { get; set; }
    }
}