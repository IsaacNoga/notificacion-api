namespace Notify.Models
{
    public class Email
    {
        public int Id { get; set; }

        public required int UserId { get; set; }
        
        public Users? User { get; set; }

        public required string UserEmail { get; set; }


    }
}
