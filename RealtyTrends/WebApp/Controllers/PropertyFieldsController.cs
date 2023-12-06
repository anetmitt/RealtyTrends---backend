using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles="admin")]
    public class PropertyFieldsController : Controller
    {
        private readonly IAppUow _uow;

        public PropertyFieldsController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: PropertyFields
        public async Task<IActionResult> Index()
        {
              return View(await _uow.PropertyFieldRepository.AllAsync());
        }

        // GET: PropertyFields/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var propertyField = await _uow.PropertyFieldRepository.FindAsync(id.Value);

            return propertyField != null ? View(propertyField) : NotFound();
        }

        // GET: PropertyFields/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PropertyFields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropertyField propertyField)
        {
            if (ModelState.IsValid)
            {
                _uow.PropertyFieldRepository.Add(propertyField);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyField);
        }

        // GET: PropertyFields/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var propertyField = await _uow.PropertyFieldRepository.FindAsync(id.Value);
            return propertyField != null ? View(propertyField) : NotFound();
        }

        // POST: PropertyFields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PropertyField propertyField)
        {
            if (id != propertyField.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PropertyFieldRepository.Update(propertyField);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyField);
        }

        // GET: PropertyFields/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var propertyField = await _uow.PropertyFieldRepository.FindAsync(id.Value);

            return propertyField != null ? View(propertyField) : NotFound();
        }

        // POST: PropertyFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        { 
            await _uow.PropertyFieldRepository.RemoveAsync(id);
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
