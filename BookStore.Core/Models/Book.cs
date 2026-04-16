
namespace BookStore.Core.Models
{
    public class Book
    {
        public const int MAX_TITLE_LENGTH = 250;
        public const int MAX_DESCRIPTION_LENGTH = 500;

        private Book(Guid id, string title, string description, decimal price) 
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
        }

        public Guid Id { get;}
        public string Title { get;  } = string.Empty;
        public string Description { get; } = string.Empty;
        public decimal Price { get; }

        public static (Book Book, string Error) Create(Guid id, string title, string description, decimal price)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                error += $"Title is empty or/and longest then {MAX_TITLE_LENGTH} symbols ";
            }
            else if (string.IsNullOrEmpty(description) || description.Length > MAX_DESCRIPTION_LENGTH)
            {
                error += $"Description is empty or/and longest then {MAX_DESCRIPTION_LENGTH} symbols ";
            }
            else if (price <= 0)
            {
                error += $"Price lower or equal 0";
            }

            var book = new Book(id, title, description, price);

            return (book,error);
        }
    }
}
