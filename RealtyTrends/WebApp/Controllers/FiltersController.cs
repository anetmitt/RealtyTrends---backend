using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles="admin")]
    public class FiltersController : Controller
    {
        private readonly IAppUow _uow;

        public FiltersController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: Filters
        public async Task<IActionResult> Index()
        {
            var filters = await _uow.FilterRepository.AllAsync();
            return View(filters);
        }

        // GET: Filters/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var filter = await _uow.FilterRepository.FindAsync(id.Value);

            return filter != null ? View(filter) : NotFound();
        }

        // GET: Filters/Create
        public async Task<IActionResult> Create()
        {
            var filterTypes = _uow.FilterTypeRepository.AllAsync();
            ViewData["FilterTypeId"] = new SelectList(await filterTypes, "Id", "Name");
            return View();
        }

        // POST: Filters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Filter filter)
        {
            if (ModelState.IsValid)
            {
                _uow.FilterRepository.Add(filter);
                
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var filterTypes = _uow.FilterTypeRepository.AllAsync();
            ViewData["FilterTypeId"] = new SelectList(await filterTypes, "Id", "Name", filter.FilterTypeId);
            return View(filter);
        }

        // GET: Filters/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var filter = await _uow.FilterRepository.FindAsync(id.Value);
            if (filter == null) { return NotFound(); }
            
            var filterTypes = _uow.FilterTypeRepository.AllAsync();
            
            ViewData["FilterTypeId"] = new SelectList(await filterTypes, "Id", "Name", filter.FilterTypeId);
            return View(filter);
        }

        // POST: Filters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Filter filter)
        {
            if (id != filter.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                _uow.FilterRepository.Update(filter);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var filterTypes = _uow.FilterTypeRepository.AllAsync();
            
            ViewData["FilterTypeId"] = new SelectList(await filterTypes, "Id", "Name", filter.FilterTypeId);
            return View(filter);
        }

        // GET: Filters/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var filter = await _uow.FilterRepository.FindAsync(id.Value);

            return filter != null ? View(filter) : NotFound();
        }

        // POST: Filters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.FilterRepository.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
