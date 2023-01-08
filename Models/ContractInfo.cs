using MongoDB.Bson;

namespace Companio.Models;

public class ContractInfo
{
    public ObjectId Id { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
}