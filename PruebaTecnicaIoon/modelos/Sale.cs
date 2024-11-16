using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaIoon.modelos
{
    public class Sale
    {
        [Key]
        public Guid SaleId { get; set; } 

        [Required]
        public DateTime SaleDate { get; set; } 

        [ForeignKey("User")]
        public Guid UserId { get; set; } 

        [ForeignKey("Commerce")]
        public Guid CommerceId { get; set; } 

        [ForeignKey("State")]
        public Guid StateId { get; set; } 
        public User User { get; set; }
        public Commerce Commerce { get; set; }
        public State State { get; set; }
        public ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
