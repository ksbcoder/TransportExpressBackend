using AutoMapper.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TransportExpress.AutoMapper;
using TransportExpress.Infrastructure.SQLAdapter;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.Infrastructure.SQLAdapter.Repositories;
using TransportExpress.Middlewares;
using TransportExpress.UseCases.IRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuration CORS

// Configuration JWT authentication
#region JWT
builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("Settings").GetSection("secretKey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration AutoMapper
builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(ConfigurationProfile));

// Configuration SQLAdapter
builder.Services.AddScoped<IUser, UserImplementation>();
builder.Services.AddScoped<IProduct, ProductImplementation>();
builder.Services.AddScoped<ITransport, TransportImplementation>();
builder.Services.AddScoped<IStorageType, StorageTypeImplementation>();
builder.Services.AddScoped<IStorage, StorageImplementation>();
builder.Services.AddScoped<ILogistic, LogisticImplementation>();

// Configuration SQL connection
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

// JWT Authentication
app.UseAuthentication();

app.UseAuthorization();

// Custom middleware
app.UseMiddleware<ErrorHandleMiddleware>();

app.MapControllers();

app.Run();
