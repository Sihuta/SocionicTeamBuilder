using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SocionicTeamBuilder.DAL.Entities;
using Task = SocionicTeamBuilder.DAL.Entities.Task;

namespace SocionicTeamBuilder.DAL.EF
{
    public partial class SocionicTeamBuilderContext : DbContext
    {
        public SocionicTeamBuilderContext()
        {
        }

        public SocionicTeamBuilderContext(DbContextOptions<SocionicTeamBuilderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Dichotomy> Dichotomies { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Enemy> Enemies { get; set; }
        public virtual DbSet<Enterprise> Enterprises { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<IotMeasurement> IoTmeasurements { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<SocionicType> SocionicTypes { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamMember> TeamMembers { get; set; }
        public virtual DbSet<Testing> Testings { get; set; }
        public virtual DbSet<TestingResult> TestingResults { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-09H4641;Database=SocionicTeamBuilder;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.HasOne(d => d.QuestionNumberNavigation)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answer_Question");
            });

            modelBuilder.Entity<Dichotomy>(entity =>
            {
                entity.HasKey(e => e.DichotomyAbbreveation);

                entity.ToTable("Dichotomy");

                entity.Property(e => e.DichotomyAbbreveation)
                    .HasMaxLength(2)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasIndex(e => e.UserId, "AK_UserID")
                    .IsUnique();

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.YearOfBirth).HasColumnType("date");

                entity.HasOne(d => d.Enterprise)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EnterpriseId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Employee_Enterprise");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Employee_User");
            });

            modelBuilder.Entity<Enemy>(entity =>
            {
                entity.ToTable("Enemy");

                entity.HasOne(d => d.Employee1)
                    .WithMany(p => p.EnemyEmployee1s)
                    .HasForeignKey(d => d.Employee1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Enemy_Employee");

                entity.HasOne(d => d.Employee2)
                    .WithMany(p => p.EnemyEmployee2s)
                    .HasForeignKey(d => d.Employee2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Enemy_Employee1");
            });

            modelBuilder.Entity<Enterprise>(entity =>
            {
                entity.ToTable("Enterprise");

                entity.Property(e => e.Activity)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Details).HasMaxLength(100);

                entity.Property(e => e.Mood)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.TeamMember)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.TeamMemberId)
                    .HasConstraintName("FK_Feedback_TeamMember");
            });

            modelBuilder.Entity<IotMeasurement>(entity =>
            {
                entity.ToTable("IoTMeasurement");

                entity.Property(e => e.DateTime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.TeamMember)
                    .WithMany(p => p.IoTmeasurements)
                    .HasForeignKey(d => d.TeamMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArduinoMeasurement_Employee");

                entity.HasOne(d => d.TeamMember)
                    .WithMany(p => p.IoTmeasurements)
                    .HasForeignKey(d => d.TeamMemberId)
                    .HasConstraintName("FK_IoTMeasurement_TeamMember");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Number);

                entity.ToTable("Question");

                entity.Property(e => e.DichotomyAbbreveation)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.DichotomyAbbreveationNavigation)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.DichotomyAbbreveation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_Dichotomy");

                entity.HasOne(d => d.Testing)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TestingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_MBTesting");
            });

            modelBuilder.Entity<SocionicType>(entity =>
            {
                entity.ToTable("SocionicType");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.JungDichotomies)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Mbvalue).HasColumnName("MBValue");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PlanningStyle)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Pseudonym)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Quadra)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RaininSigns)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.SmallGroup)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WorkingProfile)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Team");

                entity.Property(e => e.WayOfBuilding)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TeamMember>(entity =>
            {
                entity.ToTable("TeamMember");

                entity.Property(e => e.Position).HasMaxLength(50);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamMember_Employee");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_TeamMember_Task");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TeamMember_Team");
            });

            modelBuilder.Entity<Testing>(entity =>
            {
                entity.ToTable("Testing");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TestingResult>(entity =>
            {
                entity.ToTable("TestingResult");

                entity.Property(e => e.TestingDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TestingResults)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MBTestingResult_Employee");

                entity.HasOne(d => d.SocionicType)
                    .WithMany(p => p.TestingResults)
                    .HasForeignKey(d => d.SocionicTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MBTestingResult_SocionicType");

                entity.HasOne(d => d.Testing)
                    .WithMany(p => p.TestingResults)
                    .HasForeignKey(d => d.TestingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MBTestingResult_MBTesting");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
