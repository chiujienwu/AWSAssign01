using System;
using NLog.Web;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AWSASSIGN01
{
    class Program
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Program started");

            try
            {
                // get list of locations from user
                List<String> locations = new List<String>();
                string name;

                do
                {
                    Console.Write("Enter a location name: ");
                    name = Console.ReadLine();

                    if (name !="")
                    {
                        locations.Add(name);
                    }

                } while (name != "");

                foreach (var item in locations)
                {
                    Console.WriteLine(item);
                }

                // Add locations to location table in db
                var db = new Context();
                foreach (var item in locations)
                {
                    db.AddLocation(new Location() { Name = item });
                }

                // locations.Clear();

                Random _random = new Random();

                for (int d = 1; d < 2; d++)
                {
                    int numberOfEvents = _random.Next(0, 7);

                    for (int e = 0; e < numberOfEvents; e++)
                    {
                        int hours = _random.Next(0, 24);
                        int minutes = _random.Next(0, 60);
                        int seconds = _random.Next(0, 60);
                        int locationIndex = _random.Next(5, 8);
                        // string stringDate = DateTime.UtcNow.Date.AddDays(d * -1).AddHours(hours).AddMinutes(minutes).AddSeconds(seconds).ToString("yyyy-MM-dd HH:mm:ss");
                        // Console.WriteLine(stringDate + " " + locations[locationIndex]);

                        IQueryable<Location> queryLocation = db.Locations.AsQueryable().FirstOrDefault(loc => loc.LocationId == locationIndex);

                        Console.WriteLine(locationIndex + "-" + queryLocation.Name);

                        // IEnumerable<Location> QuerySyntax = from loc in db.Locations
                        //                                         where loc.LocationId == locationIndex
                        //                                         select loc;

                        // foreach (var location in QuerySyntax)
                        // {
                        //     Console.WriteLine(location.Name);
                        // }


                        //db.AddEvent(new Event() {TimeStamp = DateTime.UtcNow.Date.AddDays(d * -1).AddHours(hours).AddMinutes(minutes).AddSeconds(seconds), Location = queryLocation});


                    }
                }

                // // Create and save a new Location
                // var db = new EventContext();
                // db.AddBlog(blog);
                // logger.Info("Blog added - {name}", name);

                // // Display all Blogs from the database
                // var query = db.Blogs.OrderBy(b => b.Name);

                // Console.WriteLine("All blogs in the database:");
                // foreach (var item in query)
                // {
                //     Console.WriteLine(item.Name);
                // }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}
