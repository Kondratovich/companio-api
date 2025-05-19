namespace Companio.Models;

public class AbsenceTimeline : DatabaseObject
{
    public Guid UserId { get; set; }
    public int TotalDays { get; set; }
    public int TakenDays { get; set; }
    public int PlannedDays { get; set; }
    public List<Absence> Absences { get; set; }
}