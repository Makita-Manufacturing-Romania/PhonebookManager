using Microsoft.AspNetCore.Mvc.Rendering;
using PhonebookManager.Models;

namespace PhonebookManager.ViewModels
{
    public class RoleAndUserViewModel
    {
        //public List<AppRole> UserRoles { get; set; }
        //public AppUser User { get; set; }

        public class AppUserVM
        {
            public int Id { get; set; }
            public string? AdIdentity { get; set; }
            public string? DepartmentCode { get; set; }
            public string? DepartmentName { get; set; }
            public string? Email { get; set; }
            public string? BadgeNo { get; set; }
            public string? Name { get; set; }
            public AppUser? User { get; set; }
            public string? RoleName { get; set; }
            public AppRole? Role { get; set; }
            public SelectList? UserRoles { get; set; } 
            public List<AppRole>? UserRolesList { get; set; }
            public List<AppUser>? AppUsersList { get; set; }
        }
    }
}
