using System;
using Microsoft.EntityFrameworkCore;
using Task_Tracker.Models;

namespace Task_Tracker.Data
{
    public class TaskTrackerDbContext : DbContext
    {
        public TaskTrackerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entry> Entries { get; set; }
    }
}

