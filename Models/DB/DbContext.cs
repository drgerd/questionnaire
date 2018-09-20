using System.IO;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace _questionnaire.Models.DB
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

        public DbSet<User> Users { get; set; }
        public DbSet<UserToQuestionLink> UserToQuestionLinks { get; set; }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<LevelsForStage> LevelsForStages { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasKey(x=>x.Id);
            modelBuilder.Entity<Question>()
            .HasMany(bc => bc.Answers)
            .WithOne(x => x.Question)
            .HasForeignKey(x=>x.QuestionId)
            .IsRequired();

             modelBuilder.Entity<Answer>().HasKey(x=>x.Id);


            modelBuilder.Entity<Setting>().HasKey(x=>x.Id);
            modelBuilder.Entity<Setting>()
            .HasMany(bc => bc.LevelsForStages)
            .WithOne(x => x.Setting)
            .HasForeignKey(x=>x.SettingId)
            .IsRequired();

            modelBuilder.Entity<LevelsForStage>().HasKey(x=>x.Id);

            modelBuilder.Entity<Setting>().HasData(new Setting { Id = 1, StagesCount = 3 });
            modelBuilder.Entity<LevelsForStage>().HasData(new LevelsForStage { Id = 1, StageNumber = 1, SettingId = 1, Lvl1Count = 3, Lvl2Count = 2 });
            modelBuilder.Entity<LevelsForStage>().HasData(new LevelsForStage { Id = 2, StageNumber = 2, SettingId = 1, Lvl2Count = 3, Lvl3Count = 2 });
            modelBuilder.Entity<LevelsForStage>().HasData(new LevelsForStage { Id = 3, StageNumber = 3, SettingId = 1, Lvl2Count = 1, Lvl3Count = 4 });

            modelBuilder.Entity<UserToQuestionLink>()
            .HasKey(x=>new {x.UserId,x.QuestionId});

            modelBuilder.Entity<UserToQuestionLink>()
            .HasOne(bc=>bc.User)
            .WithMany(x=>x.UserToQuestionLinks)
            .HasForeignKey(bc=>bc.UserId);
            
            modelBuilder.Entity<UserToQuestionLink>()
            .HasOne(bc=>bc.Question)
            .WithMany(x=>x.UserToQuestionLinks)
            .HasForeignKey(bc=>bc.QuestionId);

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