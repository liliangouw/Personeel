using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Personeel.Models
{
    public class PersoneelContext : DbContext
    {

        public PersoneelContext()
            : base("name=PersoneelContext")
        {
            //初始化数据库
            Database.SetInitializer<PersoneelContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Regulatory> Regulatories { get; set; }
        public DbSet<Operationlog> Operationlogs { get; set; }
        public DbSet<Announce> Announces { get; set; }
        public DbSet<UserRight> UserRights { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<TrainInfo> TrainInfos { get; set; }
        public DbSet<SalaryInfo> SalaryInfos { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EncourageOrChastisement> EncourageOrChastisements { get; set; }
        public DbSet<ChangeSalary> ChangeSalaries { get; set; }
        public DbSet<AskForLeave> AskForLeaves { get; set; }
        public DbSet<Checking>Checkings { get; set; }
        public DbSet<PayRollAccount>PayRollAccounts { get; set; }
        public DbSet<Contract>Contract { get; set; }
            
    }

}