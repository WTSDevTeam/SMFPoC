using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MiniDemo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppDb");

//Add Repository Pattern
builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddDbContext<EmployeeDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();



//Add Swagger Support
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    
var app = builder.Build();


app.UseSwaggerUI();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger(x => x.SerializeAsV2 = true);
app.MapGet("/customer/{id}", ([FromServices] IDataRepository db, string id) =>
{
    return db.GetCustomerById(id);
});


app.MapGet("/customers", ([FromServices] IDataRepository db) =>
    {
        return db.GetCustomers();
    }
);

app.MapPut("/customer/{id}", ([FromServices] IDataRepository db, Customer customer) =>
{
    return db.PutCustomer(customer);
});

app.MapPost("/customer", ([FromServices] IDataRepository db, Customer customer) =>
{
    return db.AddCustomer(customer);
});

app.MapControllers();

app.Run();
