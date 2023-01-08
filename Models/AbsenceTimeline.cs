using Companio.Models.Enums;
using MongoDB.Bson;

namespace Companio.Models;

public class AbsenceTimeline
{
    public ObjectId Id { get; set; }
    public ObjectId UserId { get; set; }
    public int TotalDays { get; set; }
    public int TakenDays { get; set; }
    public int PlannedDays { get; set; }
    public List<Absence> Absences { get; set; }
}

public class Absence
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public AbsenceType Type { get; set; }
    public string Comments { get; set; }
}