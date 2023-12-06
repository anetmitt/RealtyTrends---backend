using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles="admin")]
    public class PropertyTypesController : Controller
    {
        private readonly IAppUow _uow;

        public PropertyTypesController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: PropertyTypes
        public async Task<IActionResult> Index()
        {
            return View(await _uow.PropertyTypeRepository.AllAsync());
        }

        // GET: PropertyTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var propertyType = await _uow.PropertyTypeRepository.FindAsync(id.Value);

            return propertyType != null ? View(propertyType) : NotFound();
        }

        // GET: PropertyTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PropertyTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropertyType propertyType)
        {
            if (ModelState.IsValid)
            {
                _uow.PropertyTypeRepository.Add(propertyType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyType);
        }

        // GET: PropertyTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var propertyType = await _uow.PropertyTypeRepository.FindAsync(id.Value);
            
            return propertyType != null ? View(propertyType) : NotFound();
        }

        // POST: PropertyTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PropertyType propertyType)
        {
            if (id != propertyType.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                _uow.PropertyTypeRepository.Update(propertyType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyType);
        }

        // GET: PropertyTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var propertyType = await _uow.PropertyTypeRepository.FindAsync(id.Value);

            return propertyType != null ? View(propertyType) : NotFound();
        }

        // POST: PropertyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PropertyTypeRepository.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
