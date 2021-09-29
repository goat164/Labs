using AdvancedProgramming_Lesson3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedProgramming_Lesson3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            return await _context.Books
                .Select(x => BookToDTO(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(long id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return BookToDTO(book);
        }

        [HttpPost]
        [Route("UpdateBook")]
        public async Task<ActionResult<BookDto>> UpdateBook(BookDto bookDto)
        {
            var book = await _context.Books.FindAsync(bookDto.Id);
            if (book == null)
            {
                return NotFound();
            }

            book.Name = bookDto.Name;
            book.Author = bookDto.Author;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BookExists(bookDto.Id))
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(UpdateBook),
                new { id = book.Id },
                BookToDTO(book));
        }

        [HttpPost]
        [Route("CreateBook")]
        public async Task<ActionResult<Book>> CreateBook(BookDto bookDto)
        {
            var book = new Book()
            {
                Name = bookDto.Name,
                Author = bookDto.Author
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBook),
                new { id = book.Id },
                BookToDTO(book));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<People>> DeleteBook(long id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool BookExists(long id) =>
            _context.Books.Any(e => e.Id == id);

        private static BookDto BookToDTO(Book book) =>
            new BookDto()
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author
            };
    }
}
