using ClientWebApp.Server.Services.NorthwindContextService;
using ClientWebApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientWebApp.Server.Services.DataService
{
    public class CustomerService : ICustomerService 
    {
        private readonly NorthwindDbContext _context;

        public CustomerService(NorthwindDbContext context)
        {
            _context = context;
        }
        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> UpdateCustomerRecord(string customerId, Customer customer)
        {
            if (customerId != customer.CustomerId) return customer;
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
