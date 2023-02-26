using Companio.Models;
using Companio.Mongo;
using Companio.Services.Interfaces;

namespace Companio.Services;

public class CustomerService : ServiceBase<Customer>, ICustomerService
{
    public CustomerService(MongoContext mongoContext) : base(mongoContext)
    {
    }
}