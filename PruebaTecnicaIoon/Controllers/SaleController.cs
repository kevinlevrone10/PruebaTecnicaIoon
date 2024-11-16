using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaIoon.modelos.Dtos;
using PruebaTecnicaIoon.modelos;
using PruebaTecnicaIoon.Repositorio.IRepositorio;
using System.Security.Claims;

namespace PruebaTecnicaIoon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;

        public SaleController(ISaleRepository saleRepository, IUserRepository userRepository)
        {
            _saleRepository = saleRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] SaleCreationDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return NotFound("User not found");

            var activeStateId = await _userRepository.GetActiveStateIdAsync();

            var sale = new Sale
            {
                SaleDate = DateTime.UtcNow,
                UserId = userId,
                CommerceId = user.CommerceId,
                StateId = activeStateId,
                SaleDetails = dto.SaleDetails.Select(d => new SaleDetail
                {
                    Product = d.Product,
                    Quantity = d.Quantity,
                    Price = d.Price
                }).ToList()
            };

            var createdSale = await _saleRepository.CreateAsync(sale);
            return CreatedAtAction(nameof(GetSalesByUser), new { userId = createdSale.UserId }, createdSale);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetSalesByUser(Guid userId)
        {
            var sales = await _saleRepository.GetByUserIdAsync(userId);
            return Ok(sales);
        }

        [HttpGet("state/{stateId}")]
        public async Task<IActionResult> GetSalesByState(Guid stateId)
        {
            var sales = await _saleRepository.GetByStateAsync(stateId);
            return Ok(sales);
        }
    }
}
