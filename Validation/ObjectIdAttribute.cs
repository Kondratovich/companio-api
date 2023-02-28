using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Companio.Validation;

public class ObjectIdAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return ObjectId.TryParse((string)value, out _);
    }
}