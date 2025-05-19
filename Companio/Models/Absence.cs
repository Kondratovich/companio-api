using Companio.Models.Enums;

namespace Companio.Models;

public class Absence : DatabaseObject
{
    public string AbsenceName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public AbsenceType Type { get; set; }
}
