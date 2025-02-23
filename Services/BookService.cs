using System.Threading.Tasks;
using System.Collections.Generic;
using BookManagementAPI.Models;
using BookManagementAPI.Repositories;
using BookManagementAPI.Services;
using BookManagementAPI.Data;
using Microsoft.EntityFrameworkCore;



namespace BookManagementAPI.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        public async Task<(bool isSuccess, string errorMessage, Book book)> AddBookAsync(Book book)
        {
            // Validate if the book already exists (same title and author)
            bool exists = await _bookRepository.BookExistsAsync(book.Title, book.Author);
            if (exists)
            {
                return (false, "A book with the same title and author already exists.", null);
            }

            // Additional validation (e.g., missing fields) will be handled via data annotations

            var addedBook = await _bookRepository.AddBookAsync(book);
            return (true, null, addedBook);
        }


        public async Task<bool> UpdateBookAsync(int id, Book book)
        {
            if (id != book.Id) return false;

            return await _bookRepository.UpdateBookAsync(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.SoftDeleteBookAsync(id);
        }
    }
}