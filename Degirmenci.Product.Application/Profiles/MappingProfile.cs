using Application;
using Application.Response;
using AutoMapper;
using Degirmenci.Product.Application.Commands.Create;
using Degirmenci.Product.Application.Commands.Delete;
using Degirmenci.Product.Application.Commands.Update.UpdateProduct;
using Degirmenci.Product.Application.Queries.GetById;
using Degirmenci.Product.Application.Queries.GetList;
using DegirmenciGida.Product.Application;
using DegirmenciGida.Product.Domain;

namespace Degirmenci.Product.Application.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Products, AddProductCommand>().ReverseMap();
            CreateMap<Products, AddProductResponse>().ReverseMap();
            CreateMap<Products, GenericServiceResponse<AddProductResponse>>().ReverseMap();


            CreateMap<Products,DeleteProductResponse>().ReverseMap();
            CreateMap<Products,GenericServiceResponse<DeleteProductResponse>>().ReverseMap();

            CreateMap<Products, UpdateProductCommand>().ReverseMap();
            CreateMap<Products, UpdateProductResponse>().ReverseMap();
            CreateMap<Products, GenericServiceResponse<UpdateProductResponse>>().ReverseMap();

            CreateMap<Products, GetAllProductsResponse>().ReverseMap();
            CreateMap<Products, GetListResponse<GetAllProductsResponse>>().ReverseMap();

            CreateMap<Products, GetProductByIdResponse>().ReverseMap();
            CreateMap<Products, GenericServiceResponse<GetProductByIdResponse>>().ReverseMap();

        }
    }
}
