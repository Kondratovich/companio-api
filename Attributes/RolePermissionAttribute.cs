using Companio.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Companio.Attributes;

public class RolePermissionAttribute : AuthorizeAttribute
{
    public RolePermissionAttribute(params Role[] roles)
    {
        Roles = string.Join(",", roles.Select(r => r.ToString()));
    }
}