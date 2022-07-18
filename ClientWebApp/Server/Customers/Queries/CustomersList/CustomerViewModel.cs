namespace ClientWebApp.Server.Customers.Queries.CustomersList
{
    public class CustomerViewModel
    {
        public IList<Customer> Customers { get; set; } = new List<Customer>();
    }
}
