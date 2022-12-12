using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        protected readonly IConfiguration Configuration;

        public CityInfoContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("CityInfoDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed the database with prepaired dummy data
            modelBuilder.Entity<City>().HasData(new City("New York City")
            {
                CityId = 1,
                Description = "The one with that big park."
            },
           new City("Antwerp")
           {
               CityId = 2,
               Description = "The one with the cathedral that was never really finished."
           },
           new City("Paris")
           {
               CityId = 3,
               Description = "The one with that big tower."
           });


            modelBuilder.Entity<PointOfInterest>()
          .HasData(
            new PointOfInterest("Central Park")
            {
                Id = 1,
                CityId = 1,
                Description = "The most visited urban park in the United States."
            },
            new PointOfInterest("Empire State Building")
            {
                Id = 2,
                CityId = 1,
                Description = "A 102-story skyscraper located in Midtown Manhattan."
            },
              new PointOfInterest("Cathedral")
              {
                  Id = 3,
                  CityId = 2,
                  Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans."
              },
            new PointOfInterest("Antwerp Central Station")
            {
                Id = 4,
                CityId = 2,
                Description = "The the finest example of railway architecture in Belgium."
            },
            new PointOfInterest("Eiffel Tower")
            {
                Id = 5,
                CityId = 3,
                Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel."
            },
            new PointOfInterest("The Louvre")
            {
                Id = 6,
                CityId = 3,
                Description = "The world's largest museum."
            }
            );
            base.OnModelCreating(modelBuilder);

        }


    }
}