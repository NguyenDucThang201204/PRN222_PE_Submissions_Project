using System;
using System.Collections.Generic;

namespace BOs;

public partial class BearAccount
{
    public string AccountId { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? RoleId { get; set; }
}
