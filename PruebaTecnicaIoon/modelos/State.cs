using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaIoon.modelos
{
    public class State
    {
        [Key]
        public Guid StateId { get; set; } 

        [Required]
        [MaxLength(50)]
        public string StateName { get; set; } 
    }
}
