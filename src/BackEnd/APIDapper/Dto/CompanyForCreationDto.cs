using System;
namespace MiniDemo.Model
{
    public class CompanyForCreationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public DateTime StartDate { get; set; }
    }
}