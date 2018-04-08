using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Payroll.Models
{
    public class dataContext : DbContext
    {

        public dataContext() : base("name=Payroll_Connection") { }

        public DbSet<DbModels.Tbl_Payroll> Tbl_Payroll { get; set; }
    }
}