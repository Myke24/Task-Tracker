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

        public IActionResult Add()
        {
            return View();
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entry = await _db.Entries.FirstOrDefaultAsync(x => x.Id == id);

            if (entry != null)
            {
                var viewModel = new EditViewModel()
                {
                    Id = entry.Id,
                    Title = entry.Title,
                    Description = entry.Description,
                    StartDate = entry.StartDate,
                    EndDate = entry.EndDate,
                };

                return View(viewModel);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var entry = await _db.Entries.FindAsync(model.Id);
            if (entry != null)
            {
                entry.Title = model.Title;
                entry.Description = model.Description;
                entry.StartDate = model.StartDate;
                entry.EndDate = model.EndDate;

                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            //Change this redirect to an error view because at this point an entry wasn't found and the _db returned null
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditViewModel model)
        {
            var entry = await _db.Entries.FindAsync(model.Id);
            if (entry != null)
            {
                _db.Entries.Remove(entry);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            //Change this redirect to an error view because at this point an entry wasn't found and the _db returned null
            return RedirectToAction("Index");
        }
    }
}

