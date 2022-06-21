using ClientWebApp.Shared.Models;

namespace ClientWebApp.Server.Services.DataService
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers();
    }
}
