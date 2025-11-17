using System;
using System.Collections.Generic;

namespace FA25Bear.DataAccess.Models;

public partial class BearAccount
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public string? Phone { get; set; }

    public bool? IsActive { get; set; }
}
