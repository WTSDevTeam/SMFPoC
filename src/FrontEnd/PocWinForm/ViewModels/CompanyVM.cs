using PocWinForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PocWinForm.ViewModels
{
    class CompanyVM : Company
    {

        public HttpClient client = new HttpClient();

        public CompanyVM(HttpClient source)
        {
            client = source;
        }
        public IEnumerable<Company> GetCompany(string path = "api/company")
        {
            IEnumerable<Company> Company = null;
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                Company = response.Content.ReadAsAsync<IEnumerable<Company>>().Result;
            }
            return Company;
        }

        public Uri CreateCompany(Company Company)
        {
            HttpResponseMessage response = client.PostAsJsonAsync("api/company", Company).Result;
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public Company UpdateCompany(Company Company)
        {
            HttpResponseMessage response = client.PutAsJsonAsync($"api/company/{Company.Id}", Company).Result;
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            Company = response.Content.ReadAsAsync<Company>().Result;
            return Company;
        }

        public HttpStatusCode DeleteCompany(int id)
        {
            HttpResponseMessage response = client.DeleteAsync($"api/company/{id}").Result;
            return response.StatusCode;
        }


    }
}
