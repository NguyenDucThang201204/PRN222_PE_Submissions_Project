using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_PhamThiThanhNgan.Services
{
    public enum Role
    {
        
        Administrator = 1,
        Manager = 2,
        Staff = 3,
        Member = 4
    }

    public static class ConstRoles
    {
        public const string Administrator = "administrator";
        public const string Manager = "manager";
        public const string Staff = "staff";
        public const string Member = "member";

        public const string FullRead = Manager + "," + Staff + "," + Member;
        public const string FullAccess = Manager;
    }

    public static class RoleExtensions
    {
        public static bool IsValidRole(int roleId)
        {
            return System.Enum.IsDefined(typeof(Role), roleId);
        }
        public static string GetRoleNameById(int roleId)
        {
            if (IsValidRole(roleId))
            {
                var role = (Role)roleId;
                return role.ToString().ToLowerInvariant();
            }
            return string.Empty;
        }

        public static string GetRolesString(params Role[] roles)
        {
            return string.Join(",", roles.Select(r => r.ToString().ToLowerInvariant()));
        }
    }
}
