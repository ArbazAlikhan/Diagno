using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DiagnosticMedicalCenter.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("name=DataBaseContext")
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<MedicareService> MedicareServices { get; set; }
        public DbSet<BookAppointment> BookAppointments { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<TestTable> TestTables { get; set; }
    }
}