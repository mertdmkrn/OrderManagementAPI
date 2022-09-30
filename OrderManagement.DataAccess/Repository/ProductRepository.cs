using Microsoft.EntityFrameworkCore;
using OrderManagement.DataAccess.Repository.Abstract;
using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetAllProduct()
        { 
           using(var context = new OrderManagementDbContext())
           { 
                return context.Product.ToList();
           }
        }

        public Product? GetProductById(int id)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                return context.Product.Find(id);
           }
        }

        public Product? GetProductByBarcode(string barcode)
        {
           using(var context = new OrderManagementDbContext())
           {
                return context.Product.Where(x => x.Barcode.Equals("Barcode")).FirstOrDefault();
           }
        }

        public bool DeleteProduct(Product product)
        { 
            try{
                using(var context = new OrderManagementDbContext())
                {
                    context.Product.Remove(product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }

        public Product SaveProduct(Product product)
        { 
            try{
                using(var context = new OrderManagementDbContext())
                {
                    context.Product.Add(product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return product;
        }

        public Product UpdateProduct(Product product)
        { 
            try{
                using(var context = new OrderManagementDbContext())
                {
                    context.Product.Update(product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return product;
        }
    }
}
