using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles="admin")]
    public class RegionTypesController : Controller
    {
        private readonly IAppUow _uow;

        public RegionTypesController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: RegionTypes
        public async Task<IActionResult> Index()
        {
              return View(await _uow.RegionTypeRepository.AllAsync());
        }

        // GET: RegionTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var regionType = await _uow.RegionTypeRepository.FindAsync(id.Value);

            return regionType != null ? View(regionType) : NotFound();
        }

        // GET: RegionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegionType regionType)
        {
            if (ModelState.IsValid)
            {
                _uow.RegionTypeRepository.Add(regionType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regionType);
        }

        // GET: RegionTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var regionType = await _uow.RegionTypeRepository.FindAsync(id.Value);
            
            return regionType != null ? View(regionType) : NotFound();
        }

        // POST: RegionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RegionType regionType)
        {
            if (id != regionType.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                _uow.RegionTypeRepository.Update(regionType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regionType);
        }

        // GET: RegionTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var regionType = await _uow.RegionTypeRepository.FindAsync(id.Value);

            return regionType != null ? View(regionType) : NotFound();
        }

        // POST: RegionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            await _uow.RegionTypeRepository.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
