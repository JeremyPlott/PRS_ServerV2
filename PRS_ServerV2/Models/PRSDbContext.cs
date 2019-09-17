using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PRS_ServerV2.Models {

    public class PRSDbContext : DbContext {
        public PRSDbContext(DbContextOptions<PRSDbContext> context) : base(context) { }
    }
}
