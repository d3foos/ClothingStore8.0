namespace WebApp.Models
{
    public class Products
    {
        public int     ProductID { get; set; }
        public string  Name      { get; set; }
        public decimal Price     { get; set; }
        public string  ImagePath { get; set; }
        public string  Category  { get; set; }
    }
}