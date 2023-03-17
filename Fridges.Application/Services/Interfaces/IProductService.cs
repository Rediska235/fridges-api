﻿using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Services.Services;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product GetProductById(Guid ProductId);
    Product CreateProduct(CreateProductDto Product);
    Product UpdateProduct(UpdateProductDto updateProductDto);
    void DeleteProduct(Guid ProductID);
}