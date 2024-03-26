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
            public string? AdIdentity { get; set; } // Username
            public Department? Department { get; set; }
            public string? DepartmentId { get; set; }
            public string? Email { get; set; }
            public string? BadgeNo { get; set; }
            public string? Name { get; set; }
            public AppUser? User { get; set; }
            public string? RoleName { get; set; }
            public AppRole? Role { get; set; }
            public SelectList? UserRoles { get; set; } 
            public List<AppRole>? UserRolesList { get; set; }
            public List<AppUser>? AppUsersList { get; set; }
            public List<Department>? DepartmentList { get; set; }


            public string? EmployeeID { get; set; }
            public string? LastName { get; set; }
            public string? FirstName { get; set; }
            public string? FullName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string? DepartmentCode { get; set; }
            public string? Title { get; set; }
            public string? Activity { get; set; }
            public string? Username { get; set; }
            public string? Manager { get; set; }

        }
    }
}
