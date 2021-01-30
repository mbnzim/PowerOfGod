namespace PowerOfGod.Domain.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PowerOfGod.Domain.Context;
    using PowerOfGod.Domain.Entity.Employee;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.SqlServer;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PowerOfGod.Domain.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        //===========================Add Specail Users=======================
        //SupperAdmin
        protected override void Seed(PowerOfGod.Domain.Context.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "SupperAdmin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "SupperAdmin" };

                manager.Create(role);

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var PasswordHash = new PasswordHasher();

                if (!context.Users.Any(u => u.UserName == "pastor@admin.net"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = "pastor@admin.net",
                        Email = "pastor@admin.net",
                        PasswordHash = PasswordHash.HashPassword("123456")
                    };

                    UserManager.Create(user);
                    UserManager.AddToRole(user.Id, "SupperAdmin");
                }
            }

            //Administrator
            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                var store = new RoleStore<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Administrator" };

                manager.Create(role);

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var PasswordHash = new PasswordHasher();

                if (!context.Users.Any(u => u.UserName == "secretary@admin.net"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = "secretary@admin.net",
                        Email = "secretary@admin.net",
                        PasswordHash = PasswordHash.HashPassword("123456")
                    };
                    UserManager.Create(user);
                    UserManager.AddToRole(user.Id, "Administrator");
                }
            }
            //Financial-Manager
            if (!context.Roles.Any(r => r.Name == "Financial-Manager"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Financial-Manager" };
                manager.Create(role);

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var PasswordHash = new PasswordHasher();

                if (!context.Users.Any(u => u.UserName == "treasury@admin.net"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = "treasury@admin.net",
                        Email = "treasury@admin.net",
                        PasswordHash = PasswordHash.HashPassword("123456")
                    };
                    UserManager.Create(user);
                    UserManager.AddToRole(user.Id, "Financial-Manager");
                }
            }

            //=======================Pre-populate data=======================
            //department
            context.departments.AddOrUpdate(
             new Departments { deptName = "Cleaner", deptCode = 1 },
             new Departments { deptName = "Security", deptCode = 2 },
             new Departments { deptName = "Usher", deptCode = 3 },
             new Departments { deptName = "Band", deptCode = 4 },
             new Departments { deptName = "Clergy", deptCode = 5 }
             );

            //Type of contract
            context.typeOfContracts.AddOrUpdate(
            new TypeOfContracts { typeName = "Permanent", typeCode = "P", Payment = "Yes" },
            new TypeOfContracts { typeName = "Part-Time", typeCode = "Pt", Payment = "Yes" },
            new TypeOfContracts { typeName = "Volunteer", typeCode = "V", Payment = "No" }
             );

            //Types of Leaves
            context.LeaveTypes.AddOrUpdate(
            new PowerOfGod.Domain.Entity.EmployeLeave.LeaveType { type = "Vacation Leave", leaveTypeID = 1 },
            new PowerOfGod.Domain.Entity.EmployeLeave.LeaveType { type = "Sick Leave", leaveTypeID = 2 },
            new PowerOfGod.Domain.Entity.EmployeLeave.LeaveType { type = "Maternity Leave", leaveTypeID = 3 },
            new PowerOfGod.Domain.Entity.EmployeLeave.LeaveType { type = "Paternity  Leave", leaveTypeID = 4 },
            new PowerOfGod.Domain.Entity.EmployeLeave.LeaveType { type = "Unpaid  Leave", leaveTypeID = 5 }
            );

            ////Transation Code
            //context.transactionCodes.AddOrUpdate(
            //new Entity.Transactions.TransactionCode { transCode = "DON101", transTitle = "Donation" },
            //new Entity.Transactions.TransactionCode { transCode = "EXP101", transTitle = "Expense" },
            //new Entity.Transactions.TransactionCode { transCode = "SAL101", transTitle = "Sale" }
            //);
        }
    }
}
