using System.ComponentModel.DataAnnotations;

namespace Notify.Models
{
    public class NotificationLog
    {
        public int id {set; get;}

        public int userId { set; get;}

        public required int categoryId {set; get;}
        public Category Category { set; get;}

        public required string notificationType { set; get;}

        public string? message { set;get;}

        [DataType(DataType.Date)]
        public DateTime sentDate { get; set; }
    }
}
