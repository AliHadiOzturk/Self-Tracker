using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfTracker.Models
{
    public class YearModel
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //public List<MonthWrapper> Months { get; set; }
        public int Year { get; set; }
    }
}
