
using Application;
using AutoMapper;
using Degirmenci.Product.Application.Commands.Create;
using DegirmenciGida.Product.Application.Interfaces;
using DegirmenciGida.Product.Domain;
using MediatR;

namespace Degirmenci.Product.Application.Commands.Create
{
    public class AddProductCommand : IRequest<GenericServiceResponse<AddProductResponse>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public class AddProductCommandHandler:IRequestHandler<AddProductCommand,GenericServiceResponse<AddProductResponse>>
        {
            private readonly IProductService _productService;
            private readonly IMapper _mapper;

            public AddProductCommandHandler(IProductService productService, IMapper mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            public async Task<GenericServiceResponse<AddProductResponse>> Handle(AddProductCommand request, CancellationToken cancellationToken)
            {
                GenericServiceResponse<AddProductResponse> response = new GenericServiceResponse<AddProductResponse>();
                try
                {
                    Products product = _mapper.Map<Products>(request);
                    product.CreatedDate = DateTime.Now;
                    product.IsDeleted = false;
                    product = await _productService.AddAsync(product);
                    response.Data = _mapper.Map<AddProductResponse>(product);
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Errors.Add(ex.Message);
                    return response;
                }
                response.Success = true;
                response.Message = "Add product successfull!";
                return response;
            }
        }
    }
}
