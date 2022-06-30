using System;
using System.Collections.Generic;
using System.Text;
using AlphaHRM.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace AlphaHRM.DAL
{
    public class EFContext : DbContext
    {
        public EFContext()
        {

        }
        public EFContext(DbContextOptions<EFContext> options) : base(options) { }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<VacationEntity> Vacation { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-OROOU30;Initial Catalog=HRM;Integrated Security=True");
        }
    }
}
