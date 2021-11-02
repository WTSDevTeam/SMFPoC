using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace MiniDemo.Model
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<Customer>> Get();
        public Task<Customer> Get(int id);
        // public Task<Customer> Create(CompanyForCreationDto company);
        public Task Update(int id, CustomerForCreationDto customer);
        // public Task Delete(int id);

    }
}