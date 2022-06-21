using ClientWebApp.Shared.Models;
using MediatR;

namespace ClientWebApp.Server.Customers.Queries.CustomersList
{
    public class CustomerListQuery : IRequest<List<Customer>>
    {}
}
