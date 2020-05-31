using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfTracker.Models
{
    public class EventDefinition
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
