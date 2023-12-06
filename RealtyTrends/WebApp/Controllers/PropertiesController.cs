using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles="admin")]
    public class PropertiesController : Controller
    {
        private readonly IAppUow _uow;

        public PropertiesController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var properties = _uow.PropertyRepository.AllAsync();
            return View(await properties);
        }

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _uow.PropertyRepository.FindAsync(id.Value);

            return property != null ? View(property) : NotFound();
        }

        // GET: Properties/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PropertyTypeId"] = await GetPropertyTypes(null);
            ViewData["WebsiteId"] = await GetWebsites(null);
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Property property)
        {
            if (ModelState.IsValid)
            {
                _uow.PropertyRepository.Add(property);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PropertyTypeId"] = await GetPropertyTypes(property.PropertyTypeId);
            ViewData["WebsiteId"] = await GetWebsites(property.WebsiteId);
            return View(property);
        }

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var property = await _uow.PropertyRepository.FindAsync(id.Value);
            if (property == null) { return NotFound(); }

            ViewData["PropertyTypeId"] = await GetPropertyTypes(property.PropertyTypeId);
            ViewData["WebsiteId"] = await GetWebsites(property.WebsiteId);
            return View(property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Property property)
        {
            if (id != property.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PropertyRepository.Update(property);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PropertyTypeId"] = await GetPropertyTypes(property.PropertyTypeId);
            ViewData["WebsiteId"] = await GetWebsites(property.WebsiteId);
            return View(property);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _uow.PropertyRepository.FindAsync(id.Value);

            return property != null ? View(property) : NotFound();
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PropertyRepository.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<SelectList> GetPropertyTypes(Guid? selectedId)
        {
            var propertyTypes = await _uow.PropertyTypeRepository.AllAsync();
            return selectedId == null ? new SelectList(propertyTypes, "Id", "Name") :
                new SelectList(propertyTypes, "Id", "Name", selectedId);
        }

        private async Task<SelectList> GetWebsites(Guid? selectedId)
        {
            var websites = await _uow.WebsiteRepository.AllAsync();
            
            return selectedId == null ? new SelectList(websites, "Id", "Name") :
                new SelectList(websites, "Id", "Name", selectedId);;
            
        }
    }
}
