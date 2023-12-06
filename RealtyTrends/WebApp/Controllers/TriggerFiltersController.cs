using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles="admin")]
    public class TriggerFiltersController : Controller
    {
        private readonly IAppUow _uow;

        public TriggerFiltersController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: TriggerFilters
        public async Task<IActionResult> Index()
        {
            /*var test = await _uow.RegionPropertyRepository.GetPropertiesByRegionTransactionTypeAndDateRangeAsync(Guid.Parse("3b358e9d-7870-4650-a535-6214d5050019"), Guid.Parse("d07d0a43-a456-4b4c-807d-ea1d5d074a50"), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now));*/
            
            var triggerFilters = _uow.TriggerFilterRepository.AllAsync();
            return View(await triggerFilters);
        }

        // GET: TriggerFilters/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var triggerFilter = await _uow.TriggerFilterRepository.FindAsync(id.Value);

            return triggerFilter != null ? View(triggerFilter) : NotFound();
        }

        // GET: TriggerFilters/Create
        public async Task<IActionResult> Create()
        {
            ViewData["FilterId"] = await GetFilters(null);
            ViewData["TriggerId"] = await GetTriggers(null);
            return View();
        }

        // POST: TriggerFilters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TriggerFilter triggerFilter)
        {
            if (ModelState.IsValid)
            {
                _uow.TriggerFilterRepository.Add(triggerFilter);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilterId"] = await GetFilters(triggerFilter.FilterId);
            ViewData["TriggerId"] = await GetTriggers(triggerFilter.TriggerId);
            return View(triggerFilter);
        }

        // GET: TriggerFilters/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var triggerFilter = await _uow.TriggerFilterRepository.FindAsync(id.Value);
            if (triggerFilter == null) { return NotFound(); }
            
            ViewData["FilterId"] = await GetFilters(triggerFilter.FilterId);
            ViewData["TriggerId"] = await GetTriggers(triggerFilter.TriggerId);
            return View(triggerFilter);
        }

        // POST: TriggerFilters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TriggerFilter triggerFilter)
        {
            if (id != triggerFilter.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                
                _uow.TriggerFilterRepository.Update(triggerFilter);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilterId"] = await GetFilters(triggerFilter.FilterId);
            ViewData["TriggerId"] = await GetTriggers(triggerFilter.TriggerId);
            return View(triggerFilter);
        }

        // GET: TriggerFilters/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var triggerFilter = await _uow.TriggerFilterRepository.FindAsync(id.Value);

            return triggerFilter != null ? View(triggerFilter) : NotFound();
        }

        // POST: TriggerFilters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.TriggerFilterRepository.RemoveAsync(id);
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private async Task<SelectList> GetFilters(Guid? selectedId)
        {
            var filters = await _uow.FilterRepository.AllAsync();
            return selectedId == null ? new SelectList(filters, "Id", "Value") :
                new SelectList(filters, "Id", "Value", selectedId);
        }
        
        private async Task<SelectList> GetTriggers(Guid? selectedId)
        {
            var triggers = await _uow.TriggerRepository.AllAsync();
            return selectedId == null ? new SelectList(triggers, "Id", "Name") :
                new SelectList(triggers, "Id", "Name", selectedId);
        }
    }
}
