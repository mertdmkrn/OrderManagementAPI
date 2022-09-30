using OrderManagement.DataAccess;
using OrderManagement.DataAccess.Repository;
using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services
{
    public class ProductService
    {
        public ProductRepository _productRepository;

        public ProductService(){ 

            _productRepository = new ProductRepository();
        }

        public List<Product> GetAllProduct(){ 

            return _productRepository.GetAllProduct();
        }

        public Product? GetProductById(int id){ 
            
            if(id > 0)
                return _productRepository.GetProductById(id);

            throw new Exception("Id must be greater than 0.");
        }

        public Product? GetProductByBarcode(string barcode){ 

           return _productRepository.GetProductByBarcode(barcode);
        }

        public bool DeleteProduct(Product product){ 

            return _productRepository.DeleteProduct(product);
        }

        public Product SaveProduct(Product product){ 

            return _productRepository.SaveProduct(product);
        }

        public Product UpdateProduct(Product product){ 

            return _productRepository.UpdateProduct(product);
        }

    }
}
