﻿using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services.Abstract
{
    public interface IProductService
    {
		List<Product> GetAllProduct();
	
		Product? GetProductById(int id);
	
		Product? GetProductByBarcode(string barcode);
	
		bool DeleteProduct(Product product);
	
		Product SaveProduct(Product product);
	
		Product UpdateProduct(Product product);
    }
}
