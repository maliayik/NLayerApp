using Core.DTOs;

namespace Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<List<ProductWithCategoryDto>> GetProductsWitCategory();
    }
}
