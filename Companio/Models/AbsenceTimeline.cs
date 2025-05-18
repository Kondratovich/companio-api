using Companio.Models.Enums;

namespace Companio.Models;

public class AbsenceTimeline: DatabaseObject
{
    public Guid UserId { get; set; }
    public int TotalDays { get; set; }
    public int TakenDays { get; set; }
    public int PlannedDays { get; set; }
    public List<Absence> Absences { get; set; }
}

public class Absence
{
    public string AbsenceName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public AbsenceType Type { get; set; }
}