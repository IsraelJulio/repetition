using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Context
{
    public class RepetitionDbContext : DbContext
    {
        public RepetitionDbContext(DbContextOptions<RepetitionDbContext> options) : base(options)
        {
        }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Category> Category { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quiz>()
                .HasMany(x => x.Questions)
                .WithOne(q=> q.Quiz)
                .HasForeignKey(i=> i.QuizId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepetitionDbContext).Assembly);
        }
    }
}
