using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaIoon.modelos
{
    public class Commerce
    {
        [Key]
        public Guid CommerceId { get; set; }
        [Required]
        [MaxLength(100)]
        public string CommerceName { get; set; } 

        [MaxLength(200)]
        public string Address { get; set; } 

        [MaxLength(13)]
        public string RUC { get; set; } 

        [ForeignKey("State")]
        public Guid StateId { get; set; } 

        public State State { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}

