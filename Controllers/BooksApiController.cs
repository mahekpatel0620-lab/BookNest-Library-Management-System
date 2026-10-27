
using BookNest.Data;
using BookNest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksApiController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BooksApiController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/BooksApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }

        // GET: api/BooksApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound(new { message = "Book not found." });
            }

            return Ok(book);
        }

        // POST: api/BooksApi
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (book.AvailableQuantity > book.Quantity)
            {
                return BadRequest(new
                {
                    message = "Available quantity cannot exceed total quantity."
                });
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBook),
                new { id = book.BookId },
                book
            );
        }

        // PUT: api/BooksApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest(new { message = "Book ID mismatch." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (book.AvailableQuantity > book.Quantity)
            {
                return BadRequest(new
                {
                    message = "Available quantity cannot exceed total quantity."
                });
            }

            var existingBook = await _context.Books.FindAsync(id);

            if (existingBook == null)
            {
                return NotFound(new { message = "Book not found." });
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Category = book.Category;
            existingBook.ISBN = book.ISBN;
            existingBook.Quantity = book.Quantity;
            existingBook.AvailableQuantity = book.AvailableQuantity;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/BooksApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound(new { message = "Book not found." });
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

