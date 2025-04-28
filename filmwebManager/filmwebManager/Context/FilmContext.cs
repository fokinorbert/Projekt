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
    class FilmContext : DbContext
    {
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserGenres> UserGenres { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<UserMovies> UserMovies { get; set; }
        public DbSet<UserPersons> UserPersons { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<MovieActors> MovieActors { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Persons> Persons { get; set; }
        public DbSet<UserLists> UserLists { get; set; }
        public DbSet<UserlistMovies> UserlistMovies { get; set; } 


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