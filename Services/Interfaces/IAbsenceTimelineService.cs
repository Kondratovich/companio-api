using Companio.Models;

namespace Companio.Services.Interfaces;

public interface IAbsenceTimelineService : IServiceBase<AbsenceTimeline>
{
    new AbsenceTimeline Create(AbsenceTimeline absenceTimeline);
}