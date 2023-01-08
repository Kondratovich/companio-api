using Companio.Models;
using MongoDB.Bson;

namespace Companio.Services;

public interface IProjectService
{
    List<Project> GetProjects();
    Project GetProjectById(ObjectId projectId);
}