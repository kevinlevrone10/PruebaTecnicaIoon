using Microsoft.EntityFrameworkCore;
using PruebaTecnicaIoon.Data;
using PruebaTecnicaIoon.modelos;
using PruebaTecnicaIoon.Repositorio.IRepositorio;

namespace PruebaTecnicaIoon.Repositorio
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.State)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetOwnerByCommerceIdAsync(Guid commerceId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.CommerceId == commerceId && u.Role == "Owner");
        }

        public async Task<Guid> GetActiveStateIdAsync()
        {
            var activeState = await _context.States.FirstOrDefaultAsync(s => s.StateName == "Active");
            return activeState?.StateId ?? Guid.Empty;
        }
    }
}
