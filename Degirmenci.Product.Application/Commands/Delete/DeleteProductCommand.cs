
using Application;
using AutoMapper;
using Degirmenci.Product.Application.Commands.Delete;
using DegirmenciGida.Product.Application.Interfaces;
using DegirmenciGida.Product.Domain;
using MediatR;

namespace Degirmenci.Product.Application.Commands.Delete
{
    public class DeleteProductCommand : IRequest<GenericServiceResponse<DeleteProductResponse>>
    {
        public Guid Id { get; set; }

        public class DeleteProductCommandHandler:IRequestHandler<DeleteProductCommand,GenericServiceResponse<DeleteProductResponse>>
        {
            private readonly IMapper _mapper;
            private readonly IProductService _productService;

            public DeleteProductCommandHandler(IMapper mapper, IProductService productService)
            {
                _mapper = mapper;
                _productService = productService;
            }

            public async Task<GenericServiceResponse<DeleteProductResponse>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                GenericServiceResponse<DeleteProductResponse> response = new GenericServiceResponse<DeleteProductResponse>();

                try
                {
                    Products product = await _productService.GetAsync(predicate:p=>p.Id == request.Id,cancellationToken:cancellationToken);
                    await _productService.DeleteAsync(product);
                    response.Data = _mapper.Map<DeleteProductResponse>(product);
                    response.Success = true;
                    response.Message = "OK";
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
