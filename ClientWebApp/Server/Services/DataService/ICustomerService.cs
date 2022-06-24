using ClientWebApp.Shared.Models;

namespace ClientWebApp.Server.Services.DataService
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> UpdateCustomerRecord(string customerId, Customer customer);
    }
}
