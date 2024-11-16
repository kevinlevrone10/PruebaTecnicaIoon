using Microsoft.EntityFrameworkCore;
using PruebaTecnicaIoon.Data;
using PruebaTecnicaIoon.modelos;
using PruebaTecnicaIoon.Repositorio.IRepositorio;

namespace PruebaTecnicaIoon.Repositorio
{
    public class CommerceRepository: ICommerceRepository
    {
        private readonly ApplicationDbContext _context;

        public CommerceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Commerce> CreateAsync(Commerce commerce)
        {
            _context.Commerces.Add(commerce);
            await _context.SaveChangesAsync();
            return commerce;
        }

        public async Task<IEnumerable<User>> GetUsersByCommerceIdAsync(Guid commerceId)
        {
            return await _context.Users
                .Where(u => u.CommerceId == commerceId)
                .ToListAsync();
        }

        public async Task DeleteAsync(Guid commerceId)
        {
            var commerce = await _context.Commerces.FindAsync(commerceId);
            if (commerce != null)
            {
                _context.Commerces.Remove(commerce);
                await _context.SaveChangesAsync();
            }
        }

    }
}
