using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles="admin")]
    public class FilterTypesController : Controller
    {
        private readonly IAppUow _uow;

        public FilterTypesController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: FilterTypes
        public async Task<IActionResult> Index()
        {
              return View(await _uow.FilterTypeRepository.AllAsync());
        }

        // GET: FilterTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var filterType = await _uow.FilterTypeRepository.FindAsync(id.Value);

            return filterType != null ? View(filterType) : NotFound();
        }

        // GET: FilterTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FilterTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FilterType filterType)
        {
            if (ModelState.IsValid)
            {
                _uow.FilterTypeRepository.Add(filterType);
                
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filterType);
        }

        // GET: FilterTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var filterType = await _uow.FilterTypeRepository.FindAsync(id.Value);
            
            return filterType != null ? View(filterType) : NotFound();
        }

        // POST: FilterTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FilterType filterType)
        {
            if (id != filterType.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                _uow.FilterTypeRepository.Update(filterType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filterType);
        }

        // GET: FilterTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var filterType = await _uow.FilterTypeRepository.FindAsync(id.Value);

            return filterType != null ? View(filterType) : NotFound();
        }

        // POST: FilterTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
           await _uow.FilterTypeRepository.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
