using Application;
using AutoMapper;
using DegirmenciGida.Product.Application.Interfaces;
using MediatR;

namespace Degirmenci.Product.Application.Queries.GetById
{
    public class GetProductByIdQuery:IRequest<GenericServiceResponse<GetProductByIdResponse>>
    {
        public Guid Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GenericServiceResponse<GetProductByIdResponse>>
        {
            private readonly IProductService _productService;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IProductService productService, IMapper mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            public async Task<GenericServiceResponse<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                GenericServiceResponse<GetProductByIdResponse> response = new GenericServiceResponse<GetProductByIdResponse>();
                try
                {
                    var products = await _productService.GetAsync(predicate:b=>b.Id == request.Id,cancellationToken:cancellationToken);
                   
                    response.Data = _mapper.Map<GetProductByIdResponse>(products);
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
