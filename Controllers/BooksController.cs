using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BookManagementAPI.Models;
using BookManagementAPI.Services;
using System;


namespace BookManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        //  GET: Retrieve all books or titles with pagination
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBooks(
            [FromQuery] bool titlesOnly = false,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            var books = await _bookService.GetAllBooksAsync();

            // Apply pagination and order by popularity (Views)
            var pagedBooks = books
                .OrderByDescending(b => b.Views)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (titlesOnly)
            {
                // Return only book titles
                var titles = pagedBooks.Select(b => b.Title).ToList();
                return Ok(titles);
            }

            // Return full book details
            return Ok(pagedBooks);
        }

        //  GET: Retrieve a single book by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            // Increment Book Views each time the details are fetched
            book.Views += 1;
            await _bookService.UpdateBookAsync(book.Id, book);

            // Calculate Years Since Published
            int yearsSincePublished = DateTime.Now.Year - book.PublicationYear;

            // Calculate Popularity Score
            double popularityScore = (book.Views * 0.5) + (yearsSincePublished * 2);

            // Return Book with Popularity Score
            var bookWithPopularity = new
            {
                book.Id,
                book.Title,
                book.Author,
                book.PublicationYear,
                book.Views,
                PopularityScore = popularityScore
            };

            return Ok(bookWithPopularity);
        }


        //  POST: Add single or bulk books with validation
        [HttpPost]
        public async Task<IActionResult> AddBooks([FromBody] List<Book> books)
        {
            if (books == null || books.Count == 0)
            {
                return BadRequest("No books provided.");
            }

            var errors = new List<string>();

            foreach (var book in books)
            {
                // Validate each book
                if (!TryValidateModel(book))
                {
                    errors.Add($"Validation failed for book: '{book.Title}'.");
                    continue;
                }

                var (isSuccess, errorMessage, _) = await _bookService.AddBookAsync(book);
                if (!isSuccess)
                {
                    errors.Add($"Error adding book '{book.Title}': {errorMessage}");
                }
            }

            if (errors.Any())
            {
                return BadRequest(new { Message = "Some books failed to add.", Errors = errors });
            }

            return Ok($"{books.Count} book(s) added successfully.");
        }

        //  PUT: Update a book
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("Book ID mismatch.");
            }

            var success = await _bookService.UpdateBookAsync(id, book);
            if (!success)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return NoContent();
        }

        //  DELETE: Soft delete single or bulk books
        [HttpDelete]
        public async Task<IActionResult> DeleteBooks([FromBody] List<int> bookIds)
        {
            if (bookIds == null || !bookIds.Any())
            {
                return BadRequest("No book IDs provided.");
            }

            var errors = new List<string>();

            foreach (var id in bookIds)
            {
                var success = await _bookService.DeleteBookAsync(id);
                if (!success)
                {
                    errors.Add($"Book with ID {id} not found or already deleted.");
                }
            }

            if (errors.Any())
            {
                return BadRequest(new { Message = "Some books failed to delete.", Errors = errors });
            }

            return Ok($"{bookIds.Count} book(s) deleted successfully.");
        }
    }
}
