using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamProject.Enums
{
    public enum Role
    {
        User = 1,
        Admin = 2
    }
    public class RoleEnumHelper
    {
        public static string GetRoleName(int roleValue)
        {
            switch (roleValue)
            {
                case 1:
                    return "Пользователь";
                case 2:
                    return "Админ";
                default:
                    return null;
            }
        }
        public static int GetRoleValue(string role)
        {
            switch (role)
            {
                case "Пользователь":
                    return 1;
                case "Админ":
                    return 2;
                default:
                    return -1;
            }
        }
        public static IEnumerable<SelectListItem> GetRolesList()
        {
            string[] roles = new string[] { GetRoleName(1), GetRoleName(2) };
            return new MultiSelectList(roles);
        }
    }
}