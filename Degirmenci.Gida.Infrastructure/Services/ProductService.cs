using DegirmenciGida.Product.Application.Interfaces;
using DegirmenciGida.Product.Domain;
using Infrastructure.EventBus.RabbitMQEventBus;
using Infrastructure.EventBus.Request;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace DegirmenciGida.Product.Infrastructure
{
    public class ProductService : EfRepositoryBase<Products,Guid,ProductDbContext>, IProductService
    {
        public ProductService(ProductDbContext dbContext) :base(dbContext)
        {
        }

        public async Task<bool> CheckProductStockAsync(Guid productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            return product != null && product.Stock >= quantity;
        }

        public async Task<bool> DecreaseStockAsync(Guid productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }
            var stockController = CheckProductStockAsync(productId, quantity);
            if (!stockController.Result)
            {
                return false;
            }
            else
            {
                product.Stock -= quantity;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return true;
            } 
        }

        public async Task RestoreStockAsync(Guid productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Stock += quantity;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
