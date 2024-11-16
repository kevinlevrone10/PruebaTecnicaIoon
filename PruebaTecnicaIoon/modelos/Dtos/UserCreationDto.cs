using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaIoon.modelos.Dtos
{
    public class UserCreationDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Guid CommerceId { get; set; }
    }


    public class UserUpdateDto
    {
        public string Username { get; set; }
        public Guid StateId { get; set; }
    }
}
