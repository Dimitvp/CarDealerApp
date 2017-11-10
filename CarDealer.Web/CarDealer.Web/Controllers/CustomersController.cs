namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class CustomersController : Controller
    {
        private readonly ICustomerService customer;

        public CustomersController(ICustomerService customer)
        {
            this.customer = customer;
        }

        public IActionResult All( string order)
        {
            return null;
        }
    }
}
