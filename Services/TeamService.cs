using Companio.Models;
using Companio.Mongo;
using Companio.Services.Interfaces;

namespace Companio.Services;

public class TeamService : ServiceBase<Team>, ITeamService
{
    public TeamService(MongoContext mongoContext) : base(mongoContext)
    {
    }
}