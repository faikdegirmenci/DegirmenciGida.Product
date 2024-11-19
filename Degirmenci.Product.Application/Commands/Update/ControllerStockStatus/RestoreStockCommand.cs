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
    public class RestoreStockCommand:IRequest<GenericServiceResponse<bool>>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public class RestoreStockCommandHandler : IRequestHandler<RestoreStockCommand, GenericServiceResponse<bool>>
        {
            private readonly IProductService _productService;

            public RestoreStockCommandHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<GenericServiceResponse<bool>> Handle(RestoreStockCommand request, CancellationToken cancellationToken)
            {
                GenericServiceResponse<bool> response = new GenericServiceResponse<bool>();

                try
                {
                    await _productService.RestoreStockAsync(request.ProductId, request.Quantity);
                    response.Success = true;
                    response.Message = "Ok";
                    response.Data = true;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Errors.Add(ex.Message);
                    return response;
                }

                return response;
            }
        }
    }
   
}
