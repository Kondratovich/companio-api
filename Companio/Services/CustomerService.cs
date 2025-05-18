using Companio.Models;
using Companio.Services.Interfaces;

namespace Companio.Services;

public class CustomerService : ServiceBase<Customer>, ICustomerService
{
    public CustomerService() : base()
    {
    }
}