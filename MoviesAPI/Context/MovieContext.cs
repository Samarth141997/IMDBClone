using Microsoft.EntityFrameworkCore;
using MoviesData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Context
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {

        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>().HasKey(k => new { k.ActorId, k.MovieId });
            modelBuilder.Entity<ActorMovie>()
                .HasOne<Actor>(a => a.Actor)
                .WithMany(am => am.ActorMovies)
                .HasForeignKey(fk => fk.ActorId);

            modelBuilder.Entity<ActorMovie>()
                .HasOne<Movie>(m => m.Movie)
                .WithMany(am => am.ActorMovies)
                .HasForeignKey(fk => fk.MovieId);

            modelBuilder.Entity<Movie>()
                .HasOne<Producer>(p => p.Producer)
                .WithMany(m => m.Movies)
                .HasForeignKey(m => m.ProducerId);

            modelBuilder.Entity<Movie>()
                .HasData(
                    new Movie()
                    {
                        MovieId = 1,
                        Name = "Interstellar",
                        Details = "Team goes on space exploration",
                        Genre = "Sci-Fi",
                        ProducerId = 1                        
                    },
                    new Movie()
                    {
                        MovieId = 2,
                        Name = "Dark Knight",
                        Details = "Batman vs Joker",
                        Genre = "Action/Thriller",                        
                        ProducerId = 1                        
                    },
                    new Movie()
                    {
                        MovieId = 3,
                        Name = "Jurassic Park",
                        Details = "Dinosaurs cause Havoc",
                        Genre = "Action/Fantasy",                         
                        ProducerId = 2                        
                    }
                );

            modelBuilder.Entity<ActorMovie>()
                .HasData(
                    new ActorMovie()
                    {
                        ActorId = 5,
                        MovieId = 3
                    },
                    new ActorMovie()
                    {
                        ActorId = 6,
                        MovieId = 3
                    },
                    new ActorMovie()
                    {
                        ActorId = 3,
                        MovieId = 2
                    },
                    new ActorMovie()
                    {
                        ActorId = 4,
                        MovieId = 2
                    },
                    new ActorMovie()
                    {
                        ActorId = 1,
                        MovieId = 1
                    },
                    new ActorMovie()
                    {
                        ActorId = 2,
                        MovieId = 1
                    }
                );

            modelBuilder.Entity<Producer>()
                .HasData(
                    new Producer
                    {
                        Id = 1,
                        Name = "Christopher Nolan",
                        Company = "Syncopy Films Inc.",
                        Gender = "Male",
                        DateOfBirth = new DateTime(1970, 7, 30)
                    },
                    new Producer
                    {
                        Id = 2,
                        Name = "Steven Spielberg",
                        Company = "Dreamworks Pictures",
                        Gender = "Male",
                        DateOfBirth = new DateTime(1946, 12, 18)
                    }
                );

            modelBuilder.Entity<Actor>()
                .HasData(
                    new Actor()
                    {
                        Id = 1,
                        Name = "Matthew McConaughey",
                        Gender = "Male",
                        DateOfBirth = new DateTime(1969, 11, 4)
                    },
                    new Actor()
                    {
                        Id = 2,
                        Name = "Anne Hathaway",
                        Gender = "Female",
                        DateOfBirth = new DateTime(1982, 11, 12)
                    },
                    new Actor()
                    {
                        Id = 3,
                        Name = "Christian Bale",
                        Gender = "Male",
                        DateOfBirth = new DateTime(1974, 11, 30)
                    },
                    new Actor()
                    {
                        Id = 4,
                        Name = "Heath Ledger",
                        Gender = "Male",
                        DateOfBirth = new DateTime(1979, 4, 4)
                    },
                    new Actor()
                    {
                        Id = 5,
                        Name = "Laura Dern",
                        Gender = "Female",
                        DateOfBirth = new DateTime(1967, 2, 10)
                    },
                    new Actor()
                    {
                        Id = 6,
                        Name = "Sam Neill",
                        Gender = "Male",
                        DateOfBirth = new DateTime(1947, 9, 14)
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
