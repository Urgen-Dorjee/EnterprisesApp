namespace ClientWebApp.Server.Customers.Queries.CustomerDetail
{
    public class CustomerDetailQuery : IRequest<Customer?>
    {
        public string? CustomerId { get; set; }
    }
}
