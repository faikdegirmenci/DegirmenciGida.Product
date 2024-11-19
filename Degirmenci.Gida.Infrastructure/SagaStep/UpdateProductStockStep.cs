using DegirmenciGida.Product.Application.Interfaces;
using Persistence.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degirmenci.Gida.Infrastructure.SagaStep
{
    public class UpdateProductStockStep : ISagaStep<OrderRequest, bool>
    {
        private readonly IProductService _productService;

        public UpdateProductStockStep(IProductService productService)
        {
            _productService = productService;
        }
        public async Task CompensateAsync(OrderRequest request)
        {
            // Stok geri yükleme işlemi
            await _productService.RestoreStockAsync(request.ProductId,request.Quantity);
        }

        public async Task<bool> ExecuteAsync(OrderRequest request)
        {
            // Ürün stoğunu kontrol et ve güncelle
            var response = await _productService.DecreaseStockAsync(request.ProductId,request.Quantity);
            return response;
        }
    }
}
