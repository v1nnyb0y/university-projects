using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TeamProject.Models
{
    public class TeamProjectContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<Message> Messages { get; set; }

        public TeamProjectContext() : base("DefaultConnectio") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Letter>().HasMany(l => l.Receivers)
                    .WithMany(r => r.Letters)
                    .Map(t => t.MapLeftKey("LetterId")
                    .MapRightKey("ReceiverId")
                    .ToTable("LetterReceiver"));
        }
    }
}