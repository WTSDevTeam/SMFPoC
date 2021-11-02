using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace MiniDemo.Model
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContext _context;

        public CustomerRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> Get()
        {
            var query = "select * from customer";

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Customer>(query);
                return data.ToList();
            }
        }

        public async Task<Customer> Get(int id)
        {
            var query = "select * from customer where customerId = @Id";

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Customer>(query, new { id });

                return company;
            }
        }

        public async Task Update(int id, CustomerForCreationDto customer)
        {
            var query = "update customer SET name = @Name, citizenship = @Address WHERE customerId = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", customer.Name, DbType.String);
            parameters.Add("Address", customer.Citizenship, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }


    }

}
