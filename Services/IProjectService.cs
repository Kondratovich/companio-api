using Companio.Models;
using MongoDB.Bson;

namespace Companio.Services;

public interface IProjectService
{
    List<Project> GetAll();
    Project GetById(ObjectId id);
    Project Create(Project project);
    void Update(ObjectId id, Project student);
    void Delete(ObjectId id);
}