using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaIoon.modelos
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } 

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } 

        [Required]
        public string Password { get; set; } 

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } 

        [ForeignKey("Commerce")]
        public Guid CommerceId { get; set; } 

        [ForeignKey("State")]
        public Guid StateId { get; set; } 

        public Commerce Commerce { get; set; }
        public State State { get; set; }

        public ICollection<Sale> Sales { get; set; }

    }
}
