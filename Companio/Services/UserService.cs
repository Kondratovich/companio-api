using Companio.Models;
using Companio.Services.Interfaces;

namespace Companio.Services;

public class UserService : ServiceBase<User>, IUserService
{
    public UserService() : base()
    {
    }
}