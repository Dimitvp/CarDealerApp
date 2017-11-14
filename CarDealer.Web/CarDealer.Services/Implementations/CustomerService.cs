namespace CarDealer.Services.Implementations
{
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Services.Models.Cars;
    using Models;
    using Models.Customers;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order)
        {
            var customerQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customerQuery = customerQuery
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(c => c.Name);
                    break;
                case OrderDirection.Descending:
                    customerQuery = customerQuery
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(c => c.Name);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid order direction: {order}.");
            }

            return customerQuery
                .Select(c => new CustomerModel
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();
        }

        public CustomerTotalSalesModel TotalSalesById(int id)
            => this.db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerTotalSalesModel
                {
                    Name = c.Name,
                    IsYoungDriver = c.IsYoungDriver,
                    BoughtCars = c.Sales.Select(s => new CarPriceModel
                    {
                        Price = s.Car.Parts.Sum(p => p.Part.Price),
                        Discount = s.Discount
                    })
                })
                .FirstOrDefault();
    }
}
