using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BuildUp.Data
{
    public class Action
    {
        public Action()
        {
            TimeEntries = new HashSet<TimeEntry>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Lasting { get; set; }
        public string Note { get; set; }

        public IEnumerable<TimeEntry> TimeEntries { get; set; }

        [ScaffoldColumn(false)]
        [Timestamp]
        public Byte[] RowVersion { get; set; }
    }
}
