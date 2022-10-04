using System;
using System.ComponentModel.DataAnnotations;

namespace Task_Tracker.Models
{
    public class Entry
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

