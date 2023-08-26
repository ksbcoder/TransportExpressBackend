﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportExpress.Domain.Commands.Product;
using TransportExpress.Domain.Entities;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productUseCase;
        private readonly IMapper _mapper;

        public ProductController(IProduct productUseCase, IMapper mapper)
        {
            _productUseCase = productUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Product>> GetProducts()
        {
            return await _productUseCase.GetProductsAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductCommand product)
        {
            return await _productUseCase.CreateProductAsync(_mapper.Map<Product>(product));
        }
    }
}