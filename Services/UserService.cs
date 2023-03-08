using Companio.Models;
using Companio.Mongo;
using Companio.Services.Interfaces;

namespace Companio.Services;

public class UserService : ServiceBase<User>, IUserService
{
    public UserService(MongoContext mongoContext) : base(mongoContext)
    {
    }
}