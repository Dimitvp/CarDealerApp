namespace CarDealer.Web.Controllers
{
    using CarDealer.Services.Models;
    using CarDealer.Web.Models.Customers;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class CustomersController : Controller
    {
        private readonly ICustomerService customer;

        public CustomersController(ICustomerService customer)
        {
            this.customer = customer;
        }

        [Route("customers/all/{order}")]
        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "descending"
                ? OrderDirection.Descending
                : OrderDirection.Ascending;

            var customers = this.customer.OrderedCustomers(orderDirection);

            return View(new AllCustomersModel
            {
                Customers = customers,
                OrderDirection = orderDirection
            });
        }
    }
}
