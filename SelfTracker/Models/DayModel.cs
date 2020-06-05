using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfTracker.Models
{
    public class DayModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        //public List<Event> Events { get; set; }
        public int Day { get; set; }
        public string Emoji { get; set; }
        public int MonthId { get; set; }
    }


}
