using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles="admin")]
    public class PropertyStructuresController : Controller
    {
        private readonly IAppUow _uow;

        public PropertyStructuresController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: PropertyStructures
        public async Task<IActionResult> Index()
        {
            var propertyStructures = _uow.PropertyStructureRepository.AllAsync();
            return View(await propertyStructures);
        }

        // GET: PropertyStructures/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var propertyStructure = await _uow.PropertyStructureRepository.FindAsync(id.Value);

            return propertyStructure != null ? View(propertyStructure) : NotFound();
        }

        // GET: PropertyStructures/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PropertyFieldId"] = await GetPropertyFields(null);
            ViewData["PropertyId"] = await GetProperties(null);
            return View();
        }

        // POST: PropertyStructures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropertyStructure propertyStructure)
        {
            if (ModelState.IsValid)
            {
                _uow.PropertyStructureRepository.Add(propertyStructure);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropertyFieldId"] = await GetPropertyFields(propertyStructure.PropertyFieldId);
            ViewData["PropertyId"] = await GetProperties(propertyStructure.PropertyId);
            return View(propertyStructure);
        }

        // GET: PropertyStructures/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var propertyStructure = await _uow.PropertyStructureRepository.FindAsync(id.Value);
            if (propertyStructure == null) { return NotFound(); }
            ViewData["PropertyFieldId"] = await GetPropertyFields(propertyStructure.PropertyFieldId);
            ViewData["PropertyId"] = await GetProperties(propertyStructure.PropertyId);
            return View(propertyStructure);
        }

        // POST: PropertyStructures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PropertyStructure propertyStructure)
        {
            if (id != propertyStructure.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                _uow.PropertyStructureRepository.Update(propertyStructure);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropertyFieldId"] = await GetPropertyFields(propertyStructure.PropertyFieldId);
            ViewData["PropertyId"] = await GetProperties(propertyStructure.PropertyId);
            return View(propertyStructure);
        }

        // GET: PropertyStructures/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var propertyStructure = await _uow.PropertyStructureRepository.FindAsync(id.Value);

            return propertyStructure != null ? View(propertyStructure) : NotFound();
        }

        // POST: PropertyStructures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PropertyStructureRepository.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private async Task<SelectList> GetPropertyFields(Guid? selectedId)
        {
            var propertyFields = await _uow.PropertyFieldRepository.AllAsync();
            return selectedId == null ? new SelectList(propertyFields, "Id", "Name") :
                new SelectList(propertyFields, "Id", "Name", selectedId);
        }
        
        private async Task<SelectList> GetProperties(Guid? selectedId)
        {
            var properties = await _uow.PropertyRepository.AllAsync();
            return selectedId == null ? new SelectList(properties, "Id", "Name") :
                new SelectList(properties, "Id", "Id", selectedId);
        }
    }
}
