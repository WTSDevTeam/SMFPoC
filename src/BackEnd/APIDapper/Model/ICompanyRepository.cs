using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace MiniDemo.Model
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<Company> CreateCompany(CompanyForCreationDto company);
        public Task UpdateCompany(int id, CompanyForCreationDto company);
        public Task DeleteCompany(int id);

    }
}