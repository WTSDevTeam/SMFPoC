using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace MiniDemo.Model
{
    public class DataRepository : IDataRepository
    {
        private readonly EmployeeDbContext db;

        public DataRepository(EmployeeDbContext db)
        {
            this.db = db;
        }

        //public List<Customer> GetCustomers() => db.Customer.ToList();
        public List<Customer> GetCustomers()
        {
            return db.Customer.ToList();
        }

        public Customer PutCustomer(Customer customer)
        {
            db.Customer.Update(customer);
            db.SaveChanges();
            return db.Customer.Where(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
        }

        public List<Customer> AddCustomer(Customer customer)
        {
            db.Customer.Add(customer);
            db.SaveChanges();
            return db.Customer.ToList();
        }

        public Customer GetCustomerById(string Id)
        {
            return db.Customer.Where(x => x.CustomerId == Id).FirstOrDefault();
        }

    }
}
