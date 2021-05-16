using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinderApi.Models
{
    public enum Role
    {
        SimpleUser,
        Admin
    }
    public enum Sexuality
    {
        Male,
        Female,
        NotDefined
    }
    public enum Rate
    {
        Positive,
        Neutral,
        Negative
    }

    public enum ComplaintType
    {
        FakeAccount,
        AbbusingBehaviour,
        Aggression
    }

    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            // Database.EnsureDeleted();
            // Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder contextOptions)
        {
             contextOptions.UseSqlServer("Server = ILIYA-PC\\SQLEXPRESS; Database = KinderDatabase; Trusted_Connection = True;");
            //contextOptions.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = KinderDatabase; Trusted_Connection = True;");
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Preference> Preferences { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<BannedUsers> BannedUsers { get; set; }

    }
}
