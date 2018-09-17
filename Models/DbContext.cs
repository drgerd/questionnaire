using System.IO;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace _questionnaire.Models
{
    public class QuestionnaireContext : DbContext
    {
        public QuestionnaireContext(DbContextOptions<QuestionnaireContext> options)
            : base(options)
        {
            // base.Database.EnsureDeleted();
            // base.Database.EnsureCreated();
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> User {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
            .HasMany(x=>x.Answers)
            .WithOne(x=>x.Question)
            .IsRequired();


            // modelBuilder.Entity<CorrectQuestionToAnswer>()
            // .HasKey(t=> new {t.AnswerId, t.QuestionId});

            // modelBuilder.Entity<CorrectQuestionToAnswer>()
            // .HasOne(x=>x.Question)
            // .WithMany(x=>x.CorrectAnswers)
            // .HasForeignKey(x=>x.QuestionId);

            // modelBuilder.Entity<CorrectQuestionToAnswer>()
            // .HasOne(x=>x.Answer)
            // .WithMany(x=>x.CorrectAnswers)
            // .HasForeignKey(x=>x.QuestionId);
            
        }
    }
}