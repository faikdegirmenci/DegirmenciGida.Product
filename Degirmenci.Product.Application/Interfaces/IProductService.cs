using DegirmenciGida.Product.Domain;
using Persistence.Repositories;

namespace DegirmenciGida.Product.Application.Interfaces
{
    public interface IProductService:IAsyncRepository<Products,Guid>
    {
        Task<bool> CheckProductStockAsync(Guid productId, int quantity);
        Task<bool> DecreaseStockAsync(Guid productId, int quantity);
        Task RestoreStockAsync(Guid productId, int quantity);
    }
}
