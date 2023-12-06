using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles="admin")]
    public class RegionsController : Controller
    {
        private readonly IAppUow _uow;

        public RegionsController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: Regions
        public async Task<IActionResult> Index()
        {
            var regions = _uow.RegionRepository.AllAsync();
            return View(await regions);
        }

        // GET: Regions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var region = await _uow.RegionRepository.FindAsync(id.Value);

            return region != null ? View(region) : NotFound();
        }

        // GET: Regions/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ParentId"] = await GetParents(null);
            ViewData["RegionTypeId"] = await GetRegionTypes(null);
            return View();
        }

        // POST: Regions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Region region)
        {
            if (ModelState.IsValid)
            {
                _uow.RegionRepository.Add(region);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ParentId"] = await GetParents(region.ParentId);
            ViewData["RegionTypeId"] = await GetRegionTypes(region.RegionTypeId);
            return View(region);
        }

        // GET: Regions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var region = await _uow.RegionRepository.FindAsync(id.Value);
            if (region == null) { return NotFound(); }
            ViewData["ParentId"] = await GetParents(region.ParentId);
            ViewData["RegionTypeId"] = await GetRegionTypes(region.RegionTypeId);
            return View(region);
        }

        // POST: Regions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Region region)
        {
            if (id != region.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                
                _uow.RegionRepository.Update(region);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = await GetParents(region.ParentId);
            ViewData["RegionTypeId"] = await GetRegionTypes(region.RegionTypeId);
            return View(region);
        }

        // GET: Regions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return NotFound();}

            var region = await _uow.RegionRepository.FindAsync(id.Value);

            return region != null ? View(region) : NotFound();
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.RegionRepository.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private async Task<SelectList> GetParents(Guid? selectedId)
        {
            var regions = await _uow.RegionRepository.AllAsync();
            
            return selectedId == null ? new SelectList(regions, "Id", "Name") :
                new SelectList(regions, "Id", "Name", selectedId);;
            
        }
        
        private async Task<SelectList> GetRegionTypes(Guid? selectedId)
        {
            var regionTypes = await _uow.RegionTypeRepository.AllAsync();
            
            return selectedId == null ? new SelectList(regionTypes, "Id", "Name") :
                new SelectList(regionTypes, "Id", "Name", selectedId);;
            
        }
    }
}
