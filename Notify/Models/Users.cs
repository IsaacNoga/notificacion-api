namespace Notify.Models
{
    public class Users
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public required ICollection<UserCategory> Subscriptions { get; set; }

        public required List<string> Channels { get; set; }
    }
}
