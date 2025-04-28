using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using System.Linq;
using System.Web;
using filmwebManager.Models;

namespace filmwebManager.Context
{
    public class FilmContext : DbContext
    {
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserGenres> UserGenres { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<UserMovies> UserMovies { get; set; }
        public virtual DbSet<UserPersons> UserPersons { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<MovieActors> MovieActors { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<UserLists> UserLists { get; set; }
        public virtual DbSet<UserlistMovies> UserlistMovies { get; set; } 


        public FilmContext() : base("name=FilmContext") { }

        public FilmContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<UserGenres>()
            .HasKey(x => new { x.User_id, x.Genre_id });
            modelBuilder.Entity<UserPersons>()
            .HasKey(x => new { x.User_id, x.Person_id });
            modelBuilder.Entity<UserMovies>()
            .HasKey(x => new { x.User_id, x.Movie_id });
            modelBuilder.Entity<MovieActors>()
            .HasKey(x => new { x.Person_id, x.Movie_id });
            modelBuilder.Entity<UserlistMovies>()
            .HasKey(x => new { x.list_id, x.movie_id});
        }
    }
}