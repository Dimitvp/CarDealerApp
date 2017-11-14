namespace CarDealer.Services
{
    using System.Collections.Generic;
    using CarDealer.Services.Models;

    public interface ISupplierService
    {
        IEnumerable<SupplierModel> All(bool isImporter);
    }
}
