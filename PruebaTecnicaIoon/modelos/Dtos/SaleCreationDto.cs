using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaIoon.modelos.Dtos
{
    public class SaleCreationDto
    {
        public List<SaleDetailDto> SaleDetails { get; set; }
    }

    public class SaleDetailDto
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
