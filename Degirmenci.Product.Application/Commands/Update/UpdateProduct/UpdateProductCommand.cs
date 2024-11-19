
using Application;
using AutoMapper;
using DegirmenciGida.Product.Application.Interfaces;
using DegirmenciGida.Product.Domain;
using MediatR;

namespace Degirmenci.Product.Application.Commands.Update.UpdateProduct
{
    public class UpdateProductCommand : IRequest<GenericServiceResponse<UpdateProductResponse>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, GenericServiceResponse<UpdateProductResponse>>
        {
            private readonly IProductService _productService;
            private readonly IMapper _mapper;

            public UpdateProductCommandHandler(IProductService productService, IMapper mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            public async Task<GenericServiceResponse<UpdateProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                GenericServiceResponse<UpdateProductResponse> response = new GenericServiceResponse<UpdateProductResponse>();
                try
                {
                    var products = await _productService.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
                    products = _mapper.Map(request, products);
                    products.UpdatedDate = DateTime.Now;

                    await _productService.UpdateAsync(products);
                    response.Data = _mapper.Map<UpdateProductResponse>(products);
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Errors.Add(ex.Message);
                    return response;
                }
                response.Success = true;
                response.Message = "Updated product successful!";
                return response;
            }
        }
    }
}
