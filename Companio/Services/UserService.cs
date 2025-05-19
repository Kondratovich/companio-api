using Companio.Data;
using Companio.Models;
using Companio.Services.Interfaces;

namespace Companio.Services;

public class UserService : ServiceBase<User>, IUserService
{
    public UserService(AppDbContext dbContext) : base(dbContext)
    {
    }
}