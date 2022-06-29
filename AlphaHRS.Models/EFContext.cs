using System;
using System.Collections.Generic;
using System.Text;
using AlphaHRS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlphaHRS.DAL
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options) { }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<VacationEntity> Vacation { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"");
        }
    }
}
