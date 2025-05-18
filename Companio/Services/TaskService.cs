using Companio.Services.Interfaces;
using Task = Companio.Models.Task;

namespace Companio.Services;

public class TaskService : ServiceBase<Task>, ITaskService
{
    public TaskService() : base()
    {
    }
}