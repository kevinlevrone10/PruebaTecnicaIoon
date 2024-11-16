using Microsoft.AspNetCore.Authorization;
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

    public class CommerceController : ControllerBase
    {

        private readonly ICommerceRepository _commerceRepository;
        private readonly IUserRepository _userRepository;

        public CommerceController(ICommerceRepository commerceRepository, IUserRepository userRepository)
        {
            _commerceRepository = commerceRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommerce([FromBody] CommerceCreationDto dto)
        {
            var activeStateId = await _userRepository.GetActiveStateIdAsync();

            var commerce = new Commerce
            {
                CommerceName = dto.CommerceName,
                Address = dto.Address,
                RUC = dto.RUC,
                StateId = activeStateId
            };

            var createdCommerce = await _commerceRepository.CreateAsync(commerce);

            var owner = new User
            {
                Username = dto.OwnerUsername,
                Password = PasswordHasher.HashPassword(dto.OwnerPassword),
                Role = "Owner",
                CommerceId = createdCommerce.CommerceId,
                StateId = activeStateId
            };

            await _userRepository.CreateAsync(owner);

            return CreatedAtAction(nameof(GetUsers), new { id = createdCommerce.CommerceId }, createdCommerce);
        }

        [HttpGet("{id}/users")]
        public async Task<IActionResult> GetUsers(Guid id)
        {
            var users = await _commerceRepository.GetUsersByCommerceIdAsync(id);
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommerce(Guid id)
        {
            var owner = await _userRepository.GetOwnerByCommerceIdAsync(id);
            if (owner != null)
            {
                await _commerceRepository.DeleteAsync(owner.UserId);
            }
            await _commerceRepository.DeleteAsync(id);
            return NoContent();
        }


    }
}
