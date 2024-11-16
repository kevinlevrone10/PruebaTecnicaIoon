using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaIoon.Data;
using PruebaTecnicaIoon.modelos;
using PruebaTecnicaIoon.modelos.Dtos;
using PruebaTecnicaIoon.Repositorio.IRepositorio;

namespace PruebaTecnicaIoon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDto dto)
        {
            var activeStateId = await _userRepository.GetActiveStateIdAsync();

            var user = new User
            {
                Username = dto.Username,
                Password = PasswordHasher.HashPassword(dto.Password),
                Role = dto.Role,
                CommerceId = dto.CommerceId,
                StateId = activeStateId
            };

            var createdUser = await _userRepository.CreateAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            user.Username = dto.Username;
            user.StateId = dto.StateId;

            var updatedUser = await _userRepository.UpdateAsync(user);
            return Ok(updatedUser);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("commerce/{commerceId}/users")]
        public async Task<IActionResult> GetUsersByCommerce(Guid commerceId)
        {
            var users = await _userRepository.GetUsersByCommerceAsync(commerceId);
            if (users == null || !users.Any())
            {
                return NotFound("No usuarios pa este comercio");
            }

            return Ok(users);
        }


    }
}
