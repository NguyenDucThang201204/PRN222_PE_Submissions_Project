using System;
using System.Collections.Generic;

namespace BO;

public partial class BearAccount
{
    public int AccountId { get; set; }

    public string? Username { get; set; }

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int Role { get; set; }
}
