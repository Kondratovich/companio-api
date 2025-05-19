namespace Companio.Models;

public class AuthenticationResult : DatabaseObject
{
    public string Token { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}