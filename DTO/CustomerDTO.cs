namespace Companio.DTO;

public class CustomerDTO
{
    public string EmailAddress { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}

public class CustomerReadDTO : CustomerDTO
{
    public string Id { get; set; }
}