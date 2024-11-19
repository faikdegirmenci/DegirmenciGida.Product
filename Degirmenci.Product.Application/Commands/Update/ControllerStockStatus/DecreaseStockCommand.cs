using Application;
using DegirmenciGida.Product.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degirmenci.Product.Application.Commands.Update.ControllerStockStatus
{
    public class DecreaseStockCommand:IRequest<GenericServiceResponse<bool>>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public class DecreaseStockCommandHandler : IRequestHandler<DecreaseStockCommand, GenericServiceResponse<bool>>
        {
            private readonly IProductService _productService;

            public DecreaseStockCommandHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<GenericServiceResponse<bool>> Handle(DecreaseStockCommand request, CancellationToken cancellationToken)
            {
                GenericServiceResponse<bool> result = new GenericServiceResponse<bool>();

                try
                {
                    result.Success = true;
                    result.Message = "Ok";
                    result.Data = await _productService.DecreaseStockAsync(request.ProductId, request.Quantity);
                }
                catch (Exception ex)
                {
                    result.Errors.Add(ex.Message);
                    result.Success = false;
                    return result;
                }
                return result;
            }
        }
    }
   
}
