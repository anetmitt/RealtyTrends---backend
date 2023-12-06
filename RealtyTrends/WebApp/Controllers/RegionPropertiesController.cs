using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles="admin")]
    public class RegionPropertiesController : Controller
    {
        private readonly IAppUow _uow;

        public RegionPropertiesController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: RegionProperties
        public async Task<IActionResult> Index()
        {
            var regionProperties = _uow.RegionPropertyRepository.AllAsync();
            return View(await regionProperties);
        }

        // GET: RegionProperties/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var regionProperty = await _uow.RegionPropertyRepository.FindAsync(id.Value);

            return regionProperty != null ? View(regionProperty) : NotFound();
        }

        // GET: RegionProperties/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PropertyId"] = await GetProperties(null);
            ViewData["RegionId"] = await GetRegions(null);
            ViewData["TransactionTypeId"] = await GetTransactionTypes(null);
            return View();
        }

        // POST: RegionProperties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( RegionProperty regionProperty)
        {
            if (ModelState.IsValid)
            {
                _uow.RegionPropertyRepository.Add(regionProperty);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PropertyId"] = await GetProperties(regionProperty.PropertyId);
            ViewData["RegionId"] = await GetRegions(regionProperty.RegionId);
            ViewData["TransactionTypeId"] = await GetTransactionTypes(regionProperty.TransactionTypeId);
            return View(regionProperty);
        }

        // GET: RegionProperties/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var regionProperty = await _uow.RegionPropertyRepository.FindAsync(id.Value);
            if (regionProperty == null) { return NotFound(); }
            ViewData["PropertyId"] = await GetProperties(regionProperty.PropertyId);
            ViewData["RegionId"] = await GetRegions(regionProperty.RegionId);
            ViewData["TransactionTypeId"] = await GetTransactionTypes(regionProperty.TransactionTypeId);
            return View(regionProperty);
        }

        // POST: RegionProperties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RegionProperty regionProperty)
        {
            if (id != regionProperty.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                
                _uow.RegionPropertyRepository.Update(regionProperty);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropertyId"] = await GetProperties(regionProperty.PropertyId);
            ViewData["RegionId"] = await GetRegions(regionProperty.RegionId);
            ViewData["TransactionTypeId"] = await GetTransactionTypes(regionProperty.TransactionTypeId);
            return View(regionProperty);
        }

        // GET: RegionProperties/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var regionProperty = await _uow.RegionPropertyRepository.FindAsync(id.Value);

            return regionProperty != null ? View(regionProperty) : NotFound();
        }

        // POST: RegionProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            await _uow.RegionPropertyRepository.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private async Task<SelectList> GetProperties(Guid? selectedId)
        {
            var properties = await _uow.PropertyRepository.AllAsync();
            return selectedId == null ? new SelectList(properties, "Id", "Name") :
                new SelectList(properties, "Id", "Id", selectedId);
        }
        
        private async Task<SelectList> GetRegions(Guid? selectedId)
        {
            var regions = await _uow.RegionRepository.AllAsync();
            return selectedId == null ? new SelectList(regions, "Id", "Name") :
                new SelectList(regions, "Id", "Name", selectedId);
        }
        
        private async Task<SelectList> GetTransactionTypes(Guid? selectedId)
        {
            var transactionTypes = await _uow.TransactionTypeRepository.AllAsync();
            return selectedId == null ? new SelectList(transactionTypes, "Id", "Name") :
                new SelectList(transactionTypes, "Id", "Name", selectedId);
        }
    }
}
