using System;
using System.Collections.Generic;

namespace PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;

public partial class BearAccount
{
    public int AccountId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? RoleId { get; set; }
}
