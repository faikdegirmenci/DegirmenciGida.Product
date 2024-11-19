
using Application;
using Application.Response;
using AutoMapper;
using DegirmenciGida.Product.Application.Interfaces;
using DegirmenciGida.Product.Domain;
using MediatR;
using Persistence.Paging;

namespace Degirmenci.Product.Application.Queries.GetList
{

    public class GetAllProductsQuery : IRequest<GetListResponse<GetAllProductsResponse>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, GetListResponse<GetAllProductsResponse>>
        {
            private readonly IProductService _productService;
            private readonly IMapper _mapper;

            public GetAllProductsQueryHandler(IProductService productService, IMapper mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetAllProductsResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                Paginate<Products> products = await _productService.GetListAsync(
                                                index: request.PageRequest.PageIndex,
                                                size: request.PageRequest.PageSize,
                                                cancellationToken: cancellationToken);

                GetListResponse<GetAllProductsResponse> response = _mapper.Map<GetListResponse<GetAllProductsResponse>>(products);
                return response;
            }
        }



    }
}
