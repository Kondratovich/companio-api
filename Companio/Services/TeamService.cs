using Companio.Models;
using Companio.Services.Interfaces;

namespace Companio.Services;

public class TeamService : ServiceBase<Team>, ITeamService
{
    public TeamService() : base()
    {
    }
}