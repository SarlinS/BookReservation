using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookReservation.Models;


namespace BookReservation.Controllers
{
    // The controller class for managing API endpoints.
    [Route("api/[controller]")]
    [ApiController]
    public class BookItemsController : ControllerBase
    {
        private readonly BookContext _context;

        // Constructor to inject the BookContext dependency.
        public BookItemsController(BookContext context)
        {
            _context = context;
        }

        // GET: api/BookItems Retrieves a list of available books (not reserved).
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<BookItem>>> GetBookItems()
        {
            var availableBooks = await _context.BookItems.Where(book => !book.IsReserved).ToListAsync();
            return availableBooks;
        }

        // GET: api/BookItems/reserved Retrieves a list of reserved books.
        [HttpGet("reserved")]
        public async Task<ActionResult<IEnumerable<BookItem>>> GetReservedBooks()
        {
            var reservedBooks = await _context.BookItems
                .Where(book => book.IsReserved)
                .ToListAsync();

            return reservedBooks;
        }

        // GET: api/BookItems/{id}/statushistory Retrieves the status history of a specific book identified by its ID.
        [HttpGet("{id}/statushistory")]
        public async Task<ActionResult<List<BookStatusHistory>>> GetBookStatusHistory(long id)
        {
            var bookItem = await _context.BookItems
                .Include(bi => bi.StatusHistory)
                .FirstOrDefaultAsync(bi => bi.Id == id);

            if (bookItem == null)
            {
                return NotFound();
            }

            return bookItem.StatusHistory;
        }

        // POST: api/BookItems/RemoveReservation/{id} Removes the reservation status for a book identified by its ID.
        [HttpPost("RemoveReservation/{id}")]
        public async Task<IActionResult> RemoveReservation(long id)
        {
            var bookItem = await _context.BookItems.FindAsync(id);

            if (bookItem == null)
            {
                return NotFound();
            }

            // Save the current status before changing it
            SaveStatusHistory(bookItem);

            // Update book status
            bookItem.IsReserved = false;
            bookItem.ReservationComment = null; // Clear reservation comment

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/BookItems/Reserve/{id} Reserves a book identified by its ID.
        [HttpPost("Reserve/{id}")]
        public async Task<ActionResult> ReserveBook(long id, [FromBody] ReservationRequest request)
        {
            var bookItem = await _context.BookItems.FindAsync(id);

            if (bookItem == null)
            {
                return NotFound();
            }

            // Save the current status before changing it
            SaveStatusHistory(bookItem);

            if (bookItem.IsReserved)
            {
                return BadRequest("The book is already reserved.");
            }

            // Update book reservation status and comment
            bookItem.IsReserved = true;
            bookItem.ReservationComment = request.Comment;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to save status history
        private void SaveStatusHistory(BookItem bookItem)
        {
            if (bookItem.StatusHistory == null)
            {
                bookItem.StatusHistory = new List<BookStatusHistory>();
            }

            // Save the current status with the current timestamp
            bookItem.StatusHistory.Add(new BookStatusHistory
            {
                IsReserved = bookItem.IsReserved,
                ChangeDateTime = DateTime.UtcNow
            });
        }

// These GET, POST and DELETE endpoints are here for testing purposes. Feel free to use these if needed.
/*
        // GET: api/BookItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookItem>> GetBookItem(long id)
        {
            var bookItem = await _context.BookItems.FindAsync(id);

            if (bookItem == null)
            {
                return NotFound();
            }

            return bookItem;
        }

        // POST: api/BookItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookItem>> PostBookItem(BookItem bookItem)
        {
            _context.BookItems.Add(bookItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookItem), new { id = bookItem.Id }, bookItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var bookItem = await _context.BookItems.FindAsync(id);
            if (bookItem == null)
            {
                return NotFound();
            }

            _context.BookItems.Remove(bookItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

*/

        private bool BookItemExists(long id)
        {
            return _context.BookItems.Any(e => e.Id == id);
        }
    }
}
