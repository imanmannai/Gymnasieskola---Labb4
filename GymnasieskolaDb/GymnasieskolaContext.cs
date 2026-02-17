using System;
using System.Collections.Generic;
using System.Text;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace GymnasieskolaDb
{
public class GymnasieskolaContext : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Klass> Klass { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Kurs> Kurs { get; set; }
        public DbSet<Betyg> Betyg { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                "Data Source=DESKTOP-H6RAQ3F;Initial Catalog=Gymnasieskola;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
