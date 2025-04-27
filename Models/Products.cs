using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Products
    {
        [Key]   // 👈 This tells EF Core that this is the primary key
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        // Add any other fields you need (like Description, Category, etc.)
    }
}
