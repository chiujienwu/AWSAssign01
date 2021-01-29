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
                var db = new Context();

                int locations_count = db.Locations.Count();

                // get list of locations from user if there are no location records in table

                if (locations_count == 0)
                {
                    List<String> locations = new List<String>();

                    string name;

                    do
                    {
                        Console.Write("Enter a location name: ");
                        name = Console.ReadLine();

                        if (name != "")
                        {
                            locations.Add(name);

                            foreach (var item in locations)
                            {
                                Console.WriteLine(item);
                            }
                        }

                    } while (name != "");

                    // Add locations to location table in db

                    if (locations.Count() != 0)
                    {
                        foreach (var item in locations)
                        {
                            db.AddLocation(new Location() { Name = item });
                        }

                        logger.Info("{0} locations saved to database", locations.Count());
                        locations.Clear();
                    }

                }
                else
                {
                    logger.Info("Database already contains {0} locations", locations_count);
                }

                Location[] LocationsArray = db.Locations.ToArray();

                Random _random = new Random();

                // Console.WriteLine("Debug:  Line 52 ran");

                DateTime localDateTime = DateTime.UtcNow;

                DateTime startDateTime = localDateTime.AddMonths(-6);

                List<Event> events = new List<Event>();

                while (startDateTime < localDateTime)
                {
                    int numberOfEvents = _random.Next(0, 6);
                    SortedList<DateTime, Event> dailyEvents = new SortedList<DateTime, Event>();

                    for (int i = 0; i < numberOfEvents; i++)
                    {
                        int hours = _random.Next(0, 24);
                        int minutes = _random.Next(0, 60);
                        int seconds = _random.Next(0, 60);
                        // this is an array created from the db table locations
                        int locationIndex = _random.Next(0, LocationsArray.Count());
                        DateTime d = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, hours, minutes, seconds);
                        Event e = new Event { Flagged = false, Location = LocationsArray[locationIndex], LocationId = LocationsArray[locationIndex].LocationId, TimeStamp = d };
                        // add event to sorted list where d is key to be sorted and e is of Event type value
                        dailyEvents.Add(d, e);
                    }

                    foreach (var de in dailyEvents)
                    {
                        events.Add(de.Value);
                    }

                    startDateTime = startDateTime.AddDays(1);

                }

                // Context has method that accepts Lists of Event
                db.AddEventList(events);

                logger.Info("{0} events saved to db", events.Count());

                // Ending time
                DateTime endDateTime = DateTime.UtcNow;
                TimeSpan diff = endDateTime.Subtract(startDateTime);

                // display difference between start & end time
                Console.WriteLine($"Days: {diff.Days}");
                Console.WriteLine($"Hours: {diff.Hours}");
                Console.WriteLine($"Minutes: {diff.Minutes}");
                Console.WriteLine($"Seconds: {diff.Seconds}");
                Console.WriteLine($"Milliseconds: {diff.Milliseconds}");

                logger.Info("Program ended");

                // for (int d = 1; d <= 180; d++)
                // {
                //     int numberOfEvents = _random.Next(0, 6);
                //     // Console.WriteLine("Debug:  Line 56 ran");
                //     for (int e = 0; e < numberOfEvents; e++)
                //     {
                //         int hours = _random.Next(0, 24);
                //         int minutes = _random.Next(0, 60);
                //         int seconds = _random.Next(0, 60);
                //         int locationIndex = _random.Next(1, LocationsArray.Count());
                //         string stringDate = DateTime.UtcNow.Date.AddDays(d * -1).AddHours(hours).AddMinutes(minutes).AddSeconds(seconds).ToString("yyyy-MM-dd HH:mm:ss");



                //         // Console.WriteLine("Debug:  Line 70 ran");
                //         var results = db.Locations.Where(l => l.LocationId == locationIndex).FirstOrDefault();
                //         string locationName = results.Name.ToString();
                //         Console.WriteLine(stringDate + "-" + locationName);

                //         // foreach (var location in QuerySyntax)
                //         // {
                //         //     Console.WriteLine(location.Name);
                //         // }

                //         db.AddEvent(new Event() { Flagged = false, TimeStamp = DateTime.UtcNow.Date.AddDays(d * -1).AddHours(hours).AddMinutes(minutes).AddSeconds(seconds), Location = new Location() { LocationId = locationIndex, Name = locationName } });

                //     }
                // }

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
