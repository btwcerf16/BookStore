using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookStoreDbContext _bookStoreDbContext;

        public BooksRepository(BookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }
        public async Task<List<Book>> Get()
        {
            var bookEntities = await _bookStoreDbContext.Books
                .AsNoTracking()
                .ToListAsync();
            var books = bookEntities.Select(b => Book.Create(b.Id, b.Title, b.Description, b.Price).Book).ToList();

            return books;

        }

        public async Task<Guid> Create(Book book)
        {
            var bookEntity = new BookEntity
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price
            };
            await _bookStoreDbContext.Books.AddAsync(bookEntity);
            await _bookStoreDbContext.SaveChangesAsync();

            return bookEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
            await _bookStoreDbContext.Books
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Title, b => title)
                .SetProperty(b => b.Description, b => description)
                .SetProperty(b => b.Price, b => price)
                );
            return id;
        }
        public async Task<Guid> Delete(Guid id)
        {
            await _bookStoreDbContext.Books
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
