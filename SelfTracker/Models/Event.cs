
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SelfTracker.Models
{
    public class Event
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad boş olamaz")]
        public string Name { get; set; }
        public String Description { get; set; }
        public bool IsReminder { get; set; } = false;
        public DateTime Time { get; set; }
        public int DayId { get; set; }
        //public int MyProperty { get; set; }
    }
}
