using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task_Tracker.Models;
using Task_Tracker.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Task_Tracker.Controllers
{
    public class EntryController : Controller
    {
        private readonly TaskTrackerDbContext _db;

        public EntryController(TaskTrackerDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Entry> entries = await _db.Entries.ToListAsync();
            return View(entries);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEntryViewModel addEntryRequest)
        {
            Entry entry = new Entry()
            {
                Title = addEntryRequest.Title,
                Description = addEntryRequest.Description,
                StartDate = addEntryRequest.StartDate,
                EndDate = addEntryRequest.EndDate,
            };

           await _db.Entries.AddAsync(entry);
           await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}

