using Companio.Models;

namespace Companio.Services.Interfaces;

public interface IProjectService : IServiceBase<Project>
{
    new Project Create(Project project);
}