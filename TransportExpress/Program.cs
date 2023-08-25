using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.Infrastructure.SQLAdapter;
using TransportExpress.UseCases.IRepositories;
using TransportExpress.Infrastructure.SQLAdapter.Repositories;
using TransportExpress.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IClient, Client>();
builder.Services.AddScoped<IProduct, Product>();
//builder.Services.AddScoped<ITransport, Transport>();
//builder.Services.AddScoped<IStorage, Storage>();
//builder.Services.AddScoped<IStorageType, StorageType>();


builder.Services.AddTransient<IDbConnectionBuilder>(e =>
{
    return new DbConnectionBuilder(builder.Configuration.GetConnectionString("urlConnectionSQL"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandleMiddleware>();

app.MapControllers();

app.Run();
