using Companio.Data;
using Companio.Models;
using Companio.Services.Interfaces;

namespace Companio.Services;

public class TeamService : ServiceBase<Team>, ITeamService
{
    public TeamService(AppDbContext dbContext) : base(dbContext)
    {
    }
}