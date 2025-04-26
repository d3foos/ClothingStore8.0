namespace WebApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PreferredSize { get; set; } = null!;
    }
}