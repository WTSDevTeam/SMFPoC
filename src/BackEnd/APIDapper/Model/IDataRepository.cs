using System.Collections.Generic;

namespace MiniDemo.Model
{
    public interface IDataRepository
    {
        List<Customer> AddCustomer(Customer employee);
        List<Customer> GetCustomers();
        Customer PutCustomer(Customer employee);
        Customer GetCustomerById(string id);
    }
}