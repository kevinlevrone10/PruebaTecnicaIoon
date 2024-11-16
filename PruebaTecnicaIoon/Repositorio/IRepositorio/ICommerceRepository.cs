using PruebaTecnicaIoon.modelos;

namespace PruebaTecnicaIoon.Repositorio.IRepositorio
{
    public interface ICommerceRepository 
    {
        Task<Commerce> CreateAsync(Commerce commerce);
        Task<IEnumerable<User>> GetUsersByCommerceIdAsync(Guid commerceId);
        Task DeleteAsync(Guid commerceId);
    }
}
