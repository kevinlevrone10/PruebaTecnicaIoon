using PruebaTecnicaIoon.modelos;

namespace PruebaTecnicaIoon.Repositorio.IRepositorio
{
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale);
        Task<IEnumerable<Sale>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Sale>> GetByStateAsync(Guid stateId);
    }
}
