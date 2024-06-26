﻿using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Products;

public class AddProductUseCase : IAddProductUseCase
{
    private readonly IProductRepository productRepository;

    public AddProductUseCase(IProductRepository productRepository){
        this.productRepository = productRepository;
    }
        
    public async Task ExecuteAsync(Product product){
        if (product == null) return;
        await this.productRepository.AddProductAsync(product);
    }
}
