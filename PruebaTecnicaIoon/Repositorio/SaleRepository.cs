using Microsoft.EntityFrameworkCore;
using PruebaTecnicaIoon.Data;
using PruebaTecnicaIoon.modelos;
using PruebaTecnicaIoon.Repositorio.IRepositorio;

namespace PruebaTecnicaIoon.Repositorio
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _context;

        public SaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<IEnumerable<Sale>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Sales
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetByStateAsync(Guid stateId)
        {
            return await _context.Sales
                .Where(s => s.StateId == stateId)
                .ToListAsync();
        }
    }
}
