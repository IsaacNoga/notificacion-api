using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Models
{
    [PrimaryKey(nameof(idUser), nameof(idCategory))]
    public class UserCategory
    {
        public int idUser { set; get; }
        public int idCategory { set; get; }
    }
}
