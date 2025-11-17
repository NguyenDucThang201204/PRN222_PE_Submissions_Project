using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public enum RoleEnum
    {
        Admin = 1,
        Manager = 2,
        Staff = 3,
        Member = 4
    }

    public static class RoleEnumExtensions
    {
        public static string? GetRoleName(this RoleEnum role)
        {
            return role switch
            {
                RoleEnum.Admin => "admin",
                RoleEnum.Manager => "manager",
                RoleEnum.Staff => "staff",
                RoleEnum.Member => "member",
                _ => null
            };
        }
    }
}
