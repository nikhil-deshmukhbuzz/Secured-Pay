using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using secured.pay.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secured.pay.api.Context
{
    public class DB_Sec_Pay : DbContext
    {
        public IConfiguration Configuration { get; }

        public DB_Sec_Pay()
        {
        }

        public DB_Sec_Pay(DbContextOptions<DB_Sec_Pay> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        
        public DbSet<PaymentStatus> PaymentStatuss { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RazorPay_Attribute> RazorPay_Attributes { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<TransactionStep> TransactionSteps { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<C_Transaction> C_Transactions { get; set; }
        public DbSet<Razorpay_Config> Razorpay_Configs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string con = "Server=database-1.cuace85fwem4.ap-south-1.rds.amazonaws.com;Database=uat_DB_Sec_Pay;User Id=admin; Password=buzz#2008;";//GetConnectionString();
            // string con = "Server=NIKHIL\\SQLEXPRESS;Database=DB_Sec_Pay;Trusted_Connection=True;";//GetConnectionString();
            optionsBuilder.UseSqlServer(con);
        }

        public static string GetConnectionString()
        {
            return Startup.ConnectionString;
        }
    }
}
