
using BookNest.Data;
using BookNest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        public async Task<IActionResult> Collection()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        public async Task<IActionResult> Manage()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (book.AvailableQuantity > book.Quantity)
            {
                ModelState.AddModelError(
                    nameof(book.AvailableQuantity),
                    "Available quantity cannot exceed total quantity."
                );
            }

            if (!ModelState.IsValid)
                return View(book);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Book added successfully!";

            return RedirectToAction(nameof(Manage));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.BookId)
                return BadRequest();

            if (book.AvailableQuantity > book.Quantity)
            {
                ModelState.AddModelError(
                    nameof(book.AvailableQuantity),
                    "Available quantity cannot exceed total quantity."
                );
            }

            if (!ModelState.IsValid)
                return View(book);

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Book updated successfully!";

            return RedirectToAction(nameof(Manage));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Book deleted successfully!";

            return RedirectToAction(nameof(Manage));
        }
    }
}

