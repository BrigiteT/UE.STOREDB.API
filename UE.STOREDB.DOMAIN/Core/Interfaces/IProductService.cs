using UE.STOREDB.DOMAIN.Core.DTO;

namespace UE.STOREDB.DOMAIN.Core.Interfaces
{
    public interface IProductService
    {
        Task<bool> Create(ProductCreateDTO productCreateDTO);
        Task<IEnumerable<ProductListDTO>> GetAll();
        Task<ProductListDTO> GetById(int id);
        Task<DetailProductProductDTO> GetByIdWtithDetailProduct(int id);
    }
}