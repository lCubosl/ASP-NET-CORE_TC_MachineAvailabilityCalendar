using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TC_MachineAvailabilityCalendar.Controllers.Data;
using TC_MachineAvailabilityCalendar.Models;

namespace TC_MachineAvailabilityCalendar.Controllers
{
    public class ItemsController : Controller
    {
        private readonly MyAppContext _context;
        public ItemsController(MyAppContext context)
        {
            _context = context;
        }

        // ITEM
        // Lists items index on /index page
        public async Task<IActionResult> Index()
        {
            var item = await _context.Items.ToListAsync();
            return View(item);
        }

        // CREATE item
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, ImageUrl")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // UPDATE item
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, ImageUrl")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // DELETE item
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // SCHEDULE
        // controller passes item with schedules
        public async Task<IActionResult> Calendar(int id)
        {
            var item = await _context.Items
                .Include(i => i.Schedules)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        // Create Schedule date
        public async Task<IActionResult> AddSchedule(int id, DateTime startDate)
        {
            var item = await _context.Items
                .Include(i => i.Schedules)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
                return NotFound();

            // check if schedule already exists for specified date
            bool dateExists = item.Schedules.Any(s => s.ScheduleDate.Date == startDate.Date);
            if (dateExists)
            {
                TempData["Error"] = "That machine is already scheduled for use in this Date.";
                return RedirectToAction("Calendar", new { id });
            }

            var newSchedule = new Schedule
            {
                ItemId = id,
                ScheduleDate = startDate
            };

            _context.Schedules.Add(newSchedule);
            await _context.SaveChangesAsync();

            return RedirectToAction("Calendar", new {id});
        }

        // confirmation delete VIEW
        [HttpGet]
        public async Task<IActionResult> ConfirmDeleteSchedule(int scheduleId)
        {
            var item = await _context.Schedules
                .Include(s => s.Item)
                .FirstOrDefaultAsync(s => s.Id == scheduleId);
            
            if (item == null) 
                return NotFound();

            return View(item);
        }

        // Delete a scheduled date
        [HttpPost]
        public async Task<IActionResult> DeleteScheduledDate(int scheduleId)
        {
            var item = await _context.Schedules.FindAsync(scheduleId);
            if (item == null) 
                return NotFound();

            int itemId = item.ItemId;

            _context.Schedules.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Calendar", new { id = itemId });
        }
    }
}
