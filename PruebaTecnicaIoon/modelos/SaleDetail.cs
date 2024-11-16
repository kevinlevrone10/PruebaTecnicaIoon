using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaIoon.modelos
{
    public class SaleDetail
    {

        [Key]
        public Guid DetailId { get; set; } 

        [ForeignKey("Sale")]
        public Guid SaleId { get; set; } 

        [Required]
        [MaxLength(100)]
        public string Product { get; set; } 

        [Required]
        public int Quantity { get; set; } 

        [Required]
        public decimal Price { get; set; } 

        public Sale Sale { get; set; }
    }
}
