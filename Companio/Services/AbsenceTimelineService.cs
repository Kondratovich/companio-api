using Companio.Data;
using Companio.Models;
using Companio.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Companio.Services;

public class AbsenceTimelineService : ServiceBase<AbsenceTimeline>, IAbsenceTimelineService
{
    private readonly IUserService _userService;

    public AbsenceTimelineService(IUserService userService, AppDbContext dbContext) : base(dbContext)
    {
        _userService = userService;
    }

    public new AbsenceTimeline Create(AbsenceTimeline absenceTimeline)
    {
        var user = _userService.SingleByIdOrDefault(absenceTimeline.UserId);
        if (user == null)
            throw new ValidationException($"User with {absenceTimeline.UserId} doesn't exist");

        if (GetAll().Any(a => a.UserId == absenceTimeline.UserId))
            throw new ValidationException($"Absence timeline for specified user {absenceTimeline.UserId} already exists");

        return base.Create(absenceTimeline);
    }
}