﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalProject.Models
{
    public partial class FootballDBContext : DbContext
    {
        public FootballDBContext()
        {
        }

        public FootballDBContext(DbContextOptions<FootballDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CommunityFavoriteVideos> CommunityFavoriteVideos { get; set; }
        public virtual DbSet<Leagues> Leagues { get; set; }
        public virtual DbSet<QuizStandings> QuizStandings { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<UserFavoriteTeams> UserFavoriteTeams { get; set; }
        public virtual DbSet<UserFavoriteVideos> UserFavoriteVideos { get; set; }
        public virtual DbSet<UserPredictions> UserPredictions { get; set; }
        public virtual DbSet<VideoComments> VideoComments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<CommunityFavoriteVideos>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmbedCode).HasMaxLength(450);

                entity.Property(e => e.VideoDate).HasColumnType("datetime");

                entity.Property(e => e.VideoTitle)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Leagues>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LeagueName).HasMaxLength(100);
            });

            modelBuilder.Entity<QuizStandings>(entity =>
            {
                entity.HasIndex(e => e.UserId);
                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QuizStandings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserQuizStanding");
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LeagueId).HasColumnName("LeagueID");

                entity.Property(e => e.TeamName).HasMaxLength(100);

                entity.HasOne(d => d.League)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.LeagueId)
                    .HasConstraintName("FK_LeagueTeam");
            });

            modelBuilder.Entity<UserFavoriteTeams>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TeamId });

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.UserFavoriteTeams)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamFavorite");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFavoriteTeams)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFavorite");
            });

            modelBuilder.Entity<UserFavoriteVideos>(entity =>
            {
                entity.HasKey(e => new { e.VideoId, e.UserId });

                entity.Property(e => e.VideoId).HasColumnName("VideoID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFavoriteVideos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserIDFavoriteVideo");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.UserFavoriteVideos)
                    .HasForeignKey(d => d.VideoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmbedFavoriteVideo");
            });

            modelBuilder.Entity<UserPredictions>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.MatchDate, e.HomeTeam })
                    .HasName("pk_userpredictions");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.MatchDate)
                    .HasColumnName("matchDate")
                    .HasMaxLength(50);

                entity.Property(e => e.HomeTeam)
                    .HasColumnName("homeTeam")
                    .HasMaxLength(50);

                entity.Property(e => e.AwayScore).HasColumnName("awayScore");

                entity.Property(e => e.AwayTeam)
                    .HasColumnName("awayTeam")
                    .HasMaxLength(50);

                entity.Property(e => e.HomeScore).HasColumnName("homeScore");

                entity.Property(e => e.MatchDay)
                    .HasColumnName("matchDay")
                    .HasMaxLength(50);

                entity.Property(e => e.MatchPick)
                    .HasColumnName("matchPick")
                    .HasMaxLength(50);

                entity.Property(e => e.MatchResult)
                    .HasColumnName("matchResult")
                    .HasMaxLength(50);

                entity.Property(e => e.Points).HasColumnName("points");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPredictions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_userpredictions");
            });

            modelBuilder.Entity<VideoComments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.Property(e => e.VideoComment)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.VideoId).HasColumnName("VideoID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VideoComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserIDVideoComments");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.VideoComments)
                    .HasForeignKey(d => d.VideoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VideoIDComments");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
