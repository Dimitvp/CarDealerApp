namespace CarDealer.Web.Controllers
{
    using Services;
    using Models.Suppliers;
    using Microsoft.AspNetCore.Mvc;

    public class SuppliersController : Controller
    {
        private const string SuppliersView = "Suppliers";
        private readonly ISupplierService supplier;

        public SuppliersController(ISupplierService supplier)
        {
            this.supplier = supplier;
        }

        public IActionResult Local()
            => View(SuppliersView, this.GetSupplierModel(false));

        public IActionResult Importers()
            => View(SuppliersView, this.GetSupplierModel(true));

        private SuppliersModel GetSupplierModel(bool importers)
        {
            var type = importers ? "Importer" : "Local";

            var supplier = this.supplier.All(importers);

            return new SuppliersModel
            {
                Type = type,
                Suppliers = supplier
            };
        }
    }
}
