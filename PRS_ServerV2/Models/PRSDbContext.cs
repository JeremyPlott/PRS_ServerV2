using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PRS_ServerV2.Models {

    public class PRSDbContext : DbContext {
        public PRSDbContext(DbContextOptions<PRSDbContext> context) : base(context) { }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<RequestLines> RequestLines { get; set; }
        public virtual DbSet<Requests> Requests { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }
    }
}
