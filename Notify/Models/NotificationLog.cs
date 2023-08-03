using System.ComponentModel.DataAnnotations;

namespace Notify.Models
{
    public class NotificationLog
    {
        public int id { set; get; }

        public int userId { set; get; }
        public Users? User { get; set; }
        public required int categoryId { set; get; }
        public Category? Category { set; get; }

        public string? notificationType { set; get; }

        public string? message { set; get; }

        [DataType(DataType.Date)]
        public DateTime sentDate { get; set; }
    }
}
