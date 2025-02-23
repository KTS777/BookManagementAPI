using System.Collections.Generic;
using System.Threading.Tasks;
using BookManagementAPI.Models;  

namespace BookManagementAPI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> SoftDeleteBookAsync(int id);
        Task<bool> BookExistsAsync(string title, string author);
    }
}
