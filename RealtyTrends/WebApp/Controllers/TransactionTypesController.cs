using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles="admin")]
    public class TransactionTypesController : Controller
    {
        private readonly IAppUow _uow;

        public TransactionTypesController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: TransactionTypes
        public async Task<IActionResult> Index()
        {
              return View(await _uow.TransactionTypeRepository.AllAsync());
        }

        // GET: TransactionTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var transactionType = await _uow.TransactionTypeRepository.FindAsync(id.Value);

            return transactionType != null ? View(transactionType) : NotFound();
        }

        // GET: TransactionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransactionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionType transactionType)
        {
            if (ModelState.IsValid)
            {
                _uow.TransactionTypeRepository.Add(transactionType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionType);
        }

        // GET: TransactionTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var transactionType = await _uow.TransactionTypeRepository.FindAsync(id.Value);
            
            return transactionType != null ? View(transactionType) : NotFound();
        }

        // POST: TransactionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TransactionType transactionType)
        {
            if (id != transactionType.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                
                _uow.TransactionTypeRepository.Update(transactionType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionType);
        }

        // GET: TransactionTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return NotFound(); }

            var transactionType = await _uow.TransactionTypeRepository.FindAsync(id.Value);

            return transactionType != null ? View(transactionType) : NotFound();
        }

        // POST: TransactionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.TransactionTypeRepository.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
