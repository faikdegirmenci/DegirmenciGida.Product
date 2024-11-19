using DegirmenciGida.Product.Application.Interfaces;
using Infrastructure.EventBus.RabbitMQEventBus;
using Infrastructure.EventBus.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degirmenci.Gida.Infrastructure.Messaging
{
    //public class ProductRequestedEventHandler : IIntegrationEventHandler<ProductRequestedEvent>
    //{
    //    private readonly Func<IProductService> _productServiceFactory;
    //    private readonly IEventBus _eventBus;

    //    public ProductRequestedEventHandler(Func<IProductService> productServiceFactory, IEventBus eventBus)
    //    {
    //        _productServiceFactory = productServiceFactory;
    //        _eventBus = eventBus;
    //    }

    //    public async Task Handle(ProductRequestedEvent @event)
    //    {
    //        // Burada factory ile servisi alıyoruz
    //        var productService = _productServiceFactory();
    //        var product = await productService.GetAsync(predicate:p=>p.Id == @event.ProductId);

    //        var productResponseEvent = new ProductResponseEvent(product.Id, product.Name, product.Price);
    //        _eventBus.Publish(productResponseEvent);
    //    }
    //}
}
