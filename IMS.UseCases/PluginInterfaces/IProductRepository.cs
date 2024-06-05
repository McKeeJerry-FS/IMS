﻿using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces;

public interface IProductRepository
{
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    Task<Product?> GetProductByIdAsync(int productId);
}
