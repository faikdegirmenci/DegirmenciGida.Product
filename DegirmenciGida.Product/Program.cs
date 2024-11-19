using DegirmenciGida.Product.Application.Interfaces;
using DegirmenciGida.Product.Domain;
using DegirmenciGida.Product.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Degirmenci.Product.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();

builder.Services.AddDbContext<ProductDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDB")));

builder.Services.AddHostedService<ProductRabbitMQService>();

builder.Services.AddScoped<IAsyncRepository<Products, Guid>, EfRepositoryBase<Products, Guid, ProductDbContext>>();
builder.Services.AddScoped<IProductService, ProductService>();

//builder.Services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
//{
//    // RabbitMQ baðlantý ayarlarý
//    var connectionFactory = new ConnectionFactory()
//    {
//        HostName = "localhost",  // RabbitMQ sunucu adresi
//        UserName = "guest",  // RabbitMQ kullanýcý adý
//        Password = "guest"   // RabbitMQ þifresi
//    };

//    var connection = connectionFactory.CreateConnection();
//    return new EventBusRabbitMQ(connection, sp);
//});

//builder.Services.AddScoped<Func<IProductService>>(sp =>
//{
//    return () => sp.GetRequiredService<IProductService>();
//});

//builder.Services.AddScoped<IIntegrationEventHandler<ProductRequestedEvent>, ProductRequestedEventHandler>();


var app = builder.Build();

//var eventBus = app.Services.GetRequiredService<IEventBus>();
//eventBus.Subscribe<ProductRequestedEvent, ProductRequestedEventHandler>();  // ProductRequestedEvent'leri dinlemek için abone oluyoruz

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
