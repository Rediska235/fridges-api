﻿using Fridges.Application.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Services.Interfaces;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product GetProductById(Guid productId);
    Product CreateProduct(CreateProductDto product);
    Product UpdateProduct(UpdateProductDto updateProductDto);
    void DeleteProduct(Guid productID);
}
