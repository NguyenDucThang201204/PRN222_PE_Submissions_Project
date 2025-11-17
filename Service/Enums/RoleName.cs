using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Enums
{
    public static class RoleName
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Staff = "Staff";
        public const string Member = "Member";

        public const string AllRoles = $"{Manager},{Staff},{Member}";
        public const string StaffRoles = $"{Admin},{Manager}";
    }

}
