using Application;
using AutoMapper;
using DegirmenciGida.Product.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degirmenci.Product.Application.Commands.Update.ControllerStockStatus
{
    public class ControllerStockStatusCommand:IRequest<GenericServiceResponse<bool>>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public class ControllerStockStatusCommandHandler : IRequestHandler<ControllerStockStatusCommand, GenericServiceResponse<bool>>
        {
            private readonly IProductService _productService;
            private readonly IMapper _mapper;

            public ControllerStockStatusCommandHandler(IProductService productService, IMapper mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            public async Task<GenericServiceResponse<bool>> Handle(ControllerStockStatusCommand request, CancellationToken cancellationToken)
            {
                GenericServiceResponse<bool> response = new GenericServiceResponse<bool>();

                try
                {
                    response.Success = true;
                    response.Message = "Ok";
                    response.Data = await _productService.CheckProductStockAsync(request.ProductId, request.Quantity);
                }
                catch (Exception ex)
                {
                    response.Errors.Add(ex.Message);
                    response.Success = false;
                    return response;
                }

                return response;
            }
        }
    }
   
}
