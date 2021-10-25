using Microsoft.EntityFrameworkCore;
using Random_Quiz.Db.Models;
using Random_Quiz.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random_Quiz.Db
{
    public class QuestionContext: DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Option> Options { get; set; }

        public QuestionContext(DbContextOptions<QuestionContext> options): base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasAlternateKey("QuestionId");
            modelBuilder.Entity<Tag>().HasAlternateKey("TagId");
            modelBuilder.Entity<Question>().HasMany(q => q.Tags).WithMany(t => t.Questions);    // Many-to-many relationship.
            modelBuilder.Entity<Question>().HasMany(q => q.Options).WithOne().HasForeignKey("QuestionId");  // One-to-many relationship.
        }
    }
}
