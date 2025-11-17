using System;
using System.Collections.Generic;

namespace PE_PRN232_FA25_DaoTrongTien_BE.DAL.Models;

public partial class BearAccount
{
    public int AccountId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public int? RoleId { get; set; }
}
