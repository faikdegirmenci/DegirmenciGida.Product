using Application;
using Application.Response;
using Degirmenci.Product.Application.Commands.Create;
using Degirmenci.Product.Application.Commands.Delete;
using Degirmenci.Product.Application.Commands.Update.ControllerStockStatus;
using Degirmenci.Product.Application.Commands.Update.UpdateProduct;
using Degirmenci.Product.Application.Queries.GetById;
using Degirmenci.Product.Application.Queries.GetList;
using DegirmenciGida.Product.Domain;
using Microsoft.AspNetCore.Mvc;
using static Degirmenci.Product.Application.Commands.Update.ControllerStockStatus.RestoreStockCommand;

namespace DegirmenciGida.Product.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody]AddProductCommand command)
        {
            GenericServiceResponse<AddProductResponse> response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            DeleteProductCommand command = new DeleteProductCommand() { Id = id };
            GenericServiceResponse<DeleteProductResponse> response = await Mediator.Send(command);
            return Ok(response);
        }
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct([FromQuery] PageRequest request)
        {
            GetAllProductsQuery query = new GetAllProductsQuery() { PageRequest =request };
            GetListResponse<GetAllProductsResponse> response = await Mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute]Guid id)
        {
            GetProductByIdQuery query = new GetProductByIdQuery() { Id = id };
            GenericServiceResponse<GetProductByIdResponse> response = await Mediator.Send(query);
            return Ok(response);
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            GenericServiceResponse<UpdateProductResponse> response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("ControllerStockStatus")]
        public async Task<IActionResult> ControllerStockStatus([FromQuery] ControllerStockStatusCommand command)
        {
            GenericServiceResponse<bool> response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("DecreaseStock")]
        public async Task<IActionResult> DecreaseStock([FromQuery] DecreaseStockCommand command)
        {
            GenericServiceResponse<bool> response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("RestoreStock")]
        public async Task<IActionResult> RestoreStock([FromQuery] RestoreStockCommand command)
        {
            GenericServiceResponse<bool> response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
