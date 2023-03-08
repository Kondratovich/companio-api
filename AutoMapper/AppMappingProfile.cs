using AutoMapper;
using Companio.DTO;
using Companio.Models;
using MongoDB.Bson;
using Task = Companio.Models.Task;

namespace Companio.AutoMapper;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        ConfigureFromDomainToDto();
        ConfigureFromDtoToDomain();
    }

    private void ConfigureFromDomainToDto()
    {
        CreateMap<Project, ProjectReadDTO>();
        CreateMap<Team, TeamReadDTO>();
        CreateMap<Customer, CustomerReadDTO>();
        CreateMap<User, UserReadDTO>();
        CreateMap<Task, TaskReadDTO>();
    }

    private void ConfigureFromDtoToDomain()
    {
        CreateMap<ProjectDTO, Project>()
            .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => ObjectId.Parse(src.CustomerId)))
            .ForMember(dest => dest.TeamId, act => act.MapFrom(src => ObjectId.Parse(src.TeamId)));
        CreateMap<TeamDTO, Team>();
        CreateMap<CustomerDTO, Customer>();
        CreateMap<UserDTO, User>();
        CreateMap<TaskDTO, Task>();
    }
}