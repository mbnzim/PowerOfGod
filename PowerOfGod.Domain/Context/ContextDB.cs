
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PowerOfGod.Domain.Entity.Booking;
using PowerOfGod.Domain.Entity.Employee;
using PowerOfGod.Domain.Entity.EmployeLeave;
using PowerOfGod.Domain.Entity.Memberss;
using PowerOfGod.Domain.Entity.Salary;
using PowerOfGod.Domain.Entity.Shopping;
using PowerOfGod.Domain.Entity.Transactions;
using PowerOfGod.Domain.Entity.UserContact;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Context
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserRole { get; set; }
        public string gender { get; set; }
        public string fullName { get; set; }
        public string IDNumber { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(): base("PowerOfGodDB4", throwIfV1Schema: false)
        {
           //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        //===============Employee============= 
        public DbSet<Employees> employees { get; set; }
        public DbSet<Departments> departments { get; set; }
        public DbSet<TypeOfContracts> typeOfContracts { get; set; }
        public DbSet<Roster> rosters { get; set; }

        //===============UserContact============= 
        public DbSet<ContactUs> contactUs { get; set; }
        public DbSet<Screenshot> screenshots { get; set; }

        //===============EmployeLeave============= 
        public DbSet<EmployeeLeavel> employeeLeavels { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }

        //===============Transactions============= 
        public DbSet<TransactionCode> transactionCodes { get; set; }
        public DbSet<Church_Expenses> Church_Expenses { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Donation> donations { get; set; }
        public DbSet<TypeOfIncome> typeOfIncomes { get; set; }
        public DbSet<TypeOfExpense_Income> typeOfExpense_Incomes { get; set; }
        public DbSet<Church_Income> church_Incomes { get; set; }

        //===============Booking================= 
        public DbSet<Pastor> pastors { get; set; }
        public DbSet<PastorBooking> pastorsBooking { get; set; }
        public DbSet<TypeOfPastor> TypeOfpastors { get; set; }
        public DbSet<PastorBookingReason> bookingReason { get; set; }
        public DbSet<PastorBookingViewModel> PastorVM { get; set; }
        public DbSet<Venue> venues { get; set; }
        public DbSet<VenueBooking> venueBooking { get; set; }
        public DbSet<VenueBookingDescription> venueBookingDescription { get; set; }
        public DbSet<VenueBookingVM> venueVM { get; set; }
        public DbSet<TypeOFVenue> TypeOFVenue { get; set; }

        //===============Member================= 
        public DbSet<Members> members { get; set; }

        //====================Shopping Cart==============
        //Stock
        public DbSet<CartDepartment> Cartdepartments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        //Temporal Shopping
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Cart_Item> Cart_Items { get; set; }
        //Shopping
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Item> Order_Items { get; set; }
        public DbSet<Order_Tracking> Order_Trackings { get; set; }

        //Address Book
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Shipping_Address> Shipping_Addresses { get; set; }
        //
        public DbSet<Payment> Payments { get; set; }
        //
        //public DbSet<Customer> Customers { get; set; }
        public DbSet<Affiliate> Affiliates { get; set; }
        public DbSet<Affiliate_Joiner> Affiliate_Joiners { get; set; }
        public DbSet<CartTransaction> CartTransactions { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Withdraw> Withdrawals { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<WithdrawLevel> WithdrawLevels { get; set; }
        //===================//Shopping Cart=================

        //===================Salary=================
        public DbSet<CheckIn> checkIns { get; set; }
        public DbSet<CheckOut> checkOuts { get; set; }
        public DbSet<JobTitle> jobTitles { get; set; }
        public DbSet<Salary> salary { get; set; }
        //===================/Salary=================

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<PowerOfGod.Domain.Entity.Event.Event> Events { get; set; }

        //public System.Data.Entity.DbSet<PowerOfGod.ViewModel.EmployeeViewModel.ContactUsViewModel> ContactUsViewModels { get; set; }

        //public System.Data.Entity.DbSet<PowerOfGod.ViewModel.EmployeeViewModel.EmployeeViewModel> EmployeeViewModels { get; set; }

        //public System.Data.Entity.DbSet<PowerOfGod.ViewModel.MemberViewModel.MemberViewModel> MemberViewModels { get; set; }

        //public System.Data.Entity.DbSet<PowerOfGod.Domain.Entity.Member.Member> Members { get; set; }
    }
}
