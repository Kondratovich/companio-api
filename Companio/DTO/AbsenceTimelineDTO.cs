using Companio.Models.Enums;

namespace Companio.DTO;

public class AbsenceTimelineDTO
{
    public string UserId { get; set; }
    public int TotalDays { get; set; }
    public int TakenDays { get; set; }
    public int PlannedDays { get; set; }
    public List<AbsenceDTO> Absences { get; set; }
}

public class AbsenceTimelineReadDTO : AbsenceTimelineDTO
{
    public string Id { get; set; }
}

public class AbsenceDTO
{
    public string AbsenceName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public AbsenceType Type { get; set; }
}