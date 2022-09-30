using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagement.DataAccess;
using OrderManagement.DataAccess.Repository;
using OrderManagement.Models;
using OrderManagement.Services;
using OrderManagement.Services.Abstract;
using OrderManagement.Services.Model;
using System.Net;
using System.Text.Json.Serialization;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        ICustomerService _customerService;
        IOrderService _orderService;
        IOrderItemService _orderItemService;
        private readonly ILogger<CustomerOrderController> _logger;

        public CustomerOrderController(ILogger<CustomerOrderController> logger)
        {
            _customerService = new CustomerService();
            _orderService = new OrderService();
            _orderItemService = new OrderItemService();
            _logger = logger;
        }

        /// <summary>
        /// Get all customer.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("customer")]
        public IActionResult GetCustomers()
        {
            List<Customer> customerList = _customerService.GetAllCustomer();
            return Ok(customerList);
        }

        /// <summary>
        /// Get customer by id.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("customer/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            if(id < 1){
                return BadRequest("Id must be greater than 0.");   
            }

            Customer? customer = _customerService.GetCustomerById(id);

            if(customer == null)
                return NotFound("Customer not found...");

            return Ok(customer);
        }

        /// <summary>
        /// Get orders by customer id.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("customer/{id}/orders")]
        public IActionResult GetOrdersByCustomerId(int id)
        {
            if(id < 1)
                return BadRequest("Id must be greater than 0.");

            List<Order> orderList = _orderService.GetOrderByCustomerId(id);
            return Ok(orderList);
        }

        /// <summary>
        /// Get order by id.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("order/{id}")]
        public IActionResult GetOrderById(int id)
        {
            if(id < 1)
                return BadRequest("Id must be greater than 0.");

            Order? order = _orderService.GetOrderById(id);

            if(order == null)
                return NotFound("Customer not found...");

            return Ok(order);
        }

        /// <summary>
        /// Get order item by order id.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("order/{id}/items")]
        public IActionResult GetOrderItems(int id)
        {
            if(id < 1)
                return BadRequest("Id must be greater than 0.");

            List<OrderItem> orderItems = _orderItemService.GetOrderItemByOrderId(id);
            return Ok(orderItems);
        }

        /// <summary>
        /// Get order item by order id and item ordinal.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("order/{id}/items/{itemOrdinal}")]
        public async Task<IActionResult> GetOrderItem(int id, int itemOrdinal)
        {
          if(itemOrdinal < 1 || id < 1)
                return BadRequest("Id and Item ordinal must be greater than 0.");

            List<OrderItem> orderItems =  _orderItemService.GetOrderItemByOrderId(id);

            if(orderItems == null || orderItems.Count == 0)
                return NotFound("Order items not found...");

            if(itemOrdinal > orderItems.Count || itemOrdinal < 1)
                return NotFound(itemOrdinal + ". order items not found...");

            return Ok(orderItems[itemOrdinal - 1]);
        }

        /// <summary>
        /// Add new customer.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("customer")]
        public IActionResult SaveCustomer([FromBody]Customer customer)
        {
            if(!ModelState.IsValid)
                return BadRequest(customer);

            List<Customer> customerList = _customerService.GetAllCustomer();
            return Ok(customerList);
        }

        /// <summary>
        /// Add new order to customer.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("customer/{id}/orders")]
        public IActionResult SaveOrder(int id, [FromBody]List<ProductInfo> productInfos)
        {
            if(id < 1)
                return BadRequest("Id must be greater than 0.");

            Customer? customer = _customerService.GetCustomerById(id);

            if(customer == null)
                return NotFound("Customer not found...");

            OrderInfo orderInfo = new OrderInfo();
            orderInfo.Customer = customer;
            orderInfo.ProductList = productInfos;

            var order = _orderService.SaveOrder(orderInfo);

            return Ok(order);
        }

        /// <summary>
        /// Update customer.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("customer")]
        public IActionResult UpdateCustomer([FromBody]Customer customer)
        {
            if(!ModelState.IsValid)
                return BadRequest(customer);

            if(_customerService.GetCustomerById(customer.Id) == null)
                return NotFound("Customer not found...");

            List<Customer> customerList = _customerService.GetAllCustomer();
            return Ok(customerList);
        }

        /// <summary>
        /// Updates the address of the customer who address the order.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("order/{id}/items/updateAddress/{address}")]
        public IActionResult UpdateOrderItem(int id, [FromBody]string address)
        {
            if(id < 1)
                return BadRequest("Id must be greater than 0.");

            if(address == null || address.Trim().Equals(""))
                return BadRequest("Address is not empty.");

            Order? order = _orderService.GetOrderById(id);

            if(order == null)
                return NotFound("Order not found...");

            order.Customer.Address = address;

            return Ok(_customerService.UpdateCustomer(order.Customer));
        }

        /// <summary>
        /// Update order item quantity by order id and item ordinal .
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("order/{id}/items/{itemOrdinal}/updateQuantity/{quantity}")]
        public IActionResult UpdateOrderItem(int id, int itemOrdinal, int quantity)
        {
            if(itemOrdinal < 1 || id < 1)
                return BadRequest("Id and Item ordinal must be greater than 0.");

            if(quantity < 1)
                return BadRequest("Quantity must be greater than 0.");

            List<OrderItem> orderItems = _orderItemService.GetOrderItemByOrderId(id);

            if(orderItems == null || orderItems.Count == 0)
                return NotFound("Order items not found...");

            if(itemOrdinal > orderItems.Count || itemOrdinal < 1)
                return NotFound(itemOrdinal + ". order items not found...");

            OrderItem orderItem = orderItems[itemOrdinal - 1];
            orderItem.Quantity = quantity;

            return Ok(_orderItemService.UpdateOrderItem(orderItem));
        }

        /// <summary>
        /// Delete customer by id.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("customer/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if(id < 1)
                return BadRequest("Id must be greater than 0.");

            Customer? deletedCustomer = _customerService.GetCustomerById(id);

            if(deletedCustomer == null)
                return NotFound("Customer not found...");

            return Ok(_customerService.DeleteCustomer(deletedCustomer));
        }

        /// <summary>
        /// Delete orders by customer id.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("customer/{id}/orders")]
        public IActionResult DeleteAllCustomerOrder(int id)
        {
            if(id < 1)
                return BadRequest("Id must be greater than 0.");

            var orderList = _orderService.GetOrderByCustomerId(id);

            if(orderList == null || orderList.Count == 0)
                return NotFound("Order not found...");

            bool deleted = _orderService.DeleteOrders(orderList);
            return Ok(deleted);
        }

        /// <summary>
        /// Delete order by id.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("order/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            if(id < 1)
                return BadRequest("Id must be greater than 0.");

            Order? order = _orderService.GetOrderById(id);

            if(order == null)
                return NotFound("Order not found...");

            bool deleted = _orderService.DeleteOrder(order);
            return Ok(deleted);
        }

        /// <summary>
        /// Delete order items by order id.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("order/{id}/items")]
        public IActionResult DeleteOrderItems(int id)
        {
            if(id < 1)
                return BadRequest("Id must be greater than 0.");

            Order? order = _orderService.GetOrderById(id);

            if(order == null)
                return NotFound("Order not found...");

            return Ok(_orderItemService.DeleteOrderItems(order.OrderItems.ToList()));
        }

        /// <summary>
        /// Delete order item by order id and item ordinal.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("order/{id}/items/{itemOrdinal}")]
        public IActionResult DeleteOrderItem(int id, int itemOrdinal)
        {
          if(itemOrdinal < 1 || id < 1)
                BadRequest("Id and Item ordinal must be greater than 0.");

            List<OrderItem> orderItems = _orderItemService.GetOrderItemByOrderId(id);

            if(orderItems == null || orderItems.Count == 0)
                return NotFound("Order items not found...");

            if(itemOrdinal > orderItems.Count || itemOrdinal < 1)
                return NotFound(itemOrdinal + ". order items not found...");

            bool deleted = _orderItemService.DeleteOrderItem(orderItems[itemOrdinal - 1]);
            return Ok(deleted);
        }

    }
}
