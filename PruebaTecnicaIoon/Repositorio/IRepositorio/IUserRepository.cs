using PruebaTecnicaIoon.modelos;

namespace PruebaTecnicaIoon.Repositorio.IRepositorio
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetByIdAsync(Guid userId);
        Task<User> UpdateAsync(User user);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetOwnerByCommerceIdAsync(Guid commerceId);
        Task<Guid> GetActiveStateIdAsync();
    }
}
