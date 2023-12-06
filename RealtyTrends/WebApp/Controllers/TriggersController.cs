using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles="admin")]
    public class TriggersController : Controller
    {
        private readonly IAppUow _uow;

        public TriggersController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: Triggers
        public async Task<IActionResult> Index()
        {
            return View(await _uow.TriggerRepository.AllAsync());
        }

        // GET: Triggers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var trigger = await _uow.TriggerRepository.FindAsync(id.Value);
            
            return trigger != null ? View(trigger) : NotFound();
        }

        // GET: Triggers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Triggers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trigger trigger)
        {
            if (ModelState.IsValid)
            {
                _uow.TriggerRepository.Add(trigger);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trigger);
        }

        // GET: Triggers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var trigger = await _uow.TriggerRepository.FindAsync(id.Value);
            
            return trigger != null ? View(trigger) : NotFound();
        }

        // POST: Triggers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Trigger trigger)
        {
            if (id != trigger.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                
                _uow.TriggerRepository.Update(trigger);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trigger);
        }

        // GET: Triggers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var trigger = await _uow.TriggerRepository.FindAsync(id.Value);

            return trigger != null ? View(trigger) : NotFound();
        }

        // POST: Triggers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
           
            await _uow.TriggerRepository.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
