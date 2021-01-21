using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWSASSIGN01
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Flagged { get; set; }
        // foreign key for location
        public int LocationId { get; set; }
        // navigation property
        public Location Location { get; set; }
    }
}