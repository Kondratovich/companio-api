using Companio.Data;
using Companio.Models;
using Companio.Services.Interfaces;

namespace Companio.Services;

public class CustomerService : ServiceBase<Customer>, ICustomerService
{
    public CustomerService(AppDbContext dbContext) : base(dbContext)
    {
    }
}