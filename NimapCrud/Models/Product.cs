using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NimapCrud.Models
{
    public class Product
    {
        [Key] 
        public int ProductId {  get; set; }
        [Required]
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        [NotMapped]
        public string? CategoryName { get; set; }

    }
}
