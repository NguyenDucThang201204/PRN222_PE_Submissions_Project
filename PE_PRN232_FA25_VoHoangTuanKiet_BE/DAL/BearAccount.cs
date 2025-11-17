using System;
using System.Collections.Generic;
using System.Data;

namespace DAL;

public partial class BearAccount
{
    public int AccountId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public int RoleId { get; set; }

    public string GetRoleName()
    {
        return RoleId switch
        {
            1 => "Admin",
            2 => "Manager",
            3 => "Staff",
            4 => "Member",
        };
    }
}
