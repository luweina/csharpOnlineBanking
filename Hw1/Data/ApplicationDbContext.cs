using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hw1.Data
{
        public class Client
        {
            [Key]
        [Display(Name = "Client ID")]
        public int clientID { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string lastName { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string firstName { get; set; }

        [Display(Name = "Email")]
        [Required]
        public string email { get; set; }

        public virtual ICollection<ClientAccount> ClientAccount { get; set; }
    }

    public class BankAccount
    {
        [Key]
        [Display(Name = "Account Number")]
        public int accountNum { get; set; }

        [Display(Name = "Account Type")]
        [Required]
        public string accountType { get; set; }

        [Display(Name = "Balance")]
        [Required]
        public decimal balance { get; set; }

        public virtual ICollection<ClientAccount> ClientAccount { get; set; }
    }

    public class ClientAccount
    {
        [Key, Column(Order = 0)]
        [Display(Name = "Client ID")]
        [Required]
        public int clientID { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "Account Number")]
        [Required]
        public int accountNum { get; set; }

        public virtual Client Client { get; set; }
        public virtual BankAccount BancAccount { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<ClientAccount> ClientAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ClientAccount>()
                .HasKey(ca => new { ca.clientID, ca.accountNum });

            builder.Entity<ClientAccount>()
                .HasOne(c => c.Client)
                .WithMany(c => c.ClientAccount)
                .HasForeignKey(fk => new { fk.clientID })
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ClientAccount>()
                .HasOne(b => b.BancAccount)
                .WithMany(b => b.ClientAccount)
                .HasForeignKey(fk => new { fk.accountNum })
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
