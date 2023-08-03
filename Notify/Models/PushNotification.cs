namespace Notify.Models
{
    public class PushNotification
    {
        public int Id { get; set; }

        public required int UserId { get; set; }

        public Users? User { get; set; }

        public required string UserName { get; set; }
    }
}
