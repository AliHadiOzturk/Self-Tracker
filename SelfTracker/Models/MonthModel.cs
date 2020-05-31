using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfTracker.Models
{
    public class MonthModel
    {
        public MonthModel()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //public List<DayWrapper> Days { get; set; }
        public int Month { get; set; }

        public int YearId { get; set; }

    }
}
