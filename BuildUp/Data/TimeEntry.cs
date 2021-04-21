using System;

namespace BuildUp.Data
{
    public class TimeEntry
    {
        public int ID { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public int ActionID { get; set; }
        public Action Action { get; set; }
    }
}
