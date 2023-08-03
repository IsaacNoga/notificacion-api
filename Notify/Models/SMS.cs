namespace Notify.Models
{
    public class SMS
    {
        public int Id { get; set; }

        public required int UserId { get; set; }

        public Users? User { get; set; }

        public required string UserPhone { get; set; }
    }
}
