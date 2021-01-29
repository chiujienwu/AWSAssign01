using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AWSASSIGN01
{
    public class Context : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Event> Events { get; set; }

        public void AddEvent(Event evt)
        {
            this.Events.Add(evt);
            this.SaveChanges();
        }

        public void AddEventList(List<Event> events)
        {
            this.Events.AddRange(events);
            this.SaveChanges();
        }

        public void AddLocation(Location loc)
        {
            this.Locations.Add(loc);
            this.SaveChanges();
        }

        public void AddLocationList(List<Location> locations)
        {
            this.Locations.AddRange(locations);
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            optionsBuilder.UseSqlServer(@config["Context:ConnectionString"]);
        }
    }
}