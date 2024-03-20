using PhonebookManager.Models;

namespace PhonebookManager.ViewModels
{
    public class DepartmentViewModel
    {
        public class DepartmentVM
        {
            public int Id { get; set; }
            public string? Code { get; set; }
            public string? Name { get; set; }
            public AppUser? Manager { get; set; }
            public int ManagerId { get; set; }
            public AppUser? Responsible { get; set; }
            public int ResponsibleId { get; set; }
            public List<PhoneLine>? Lines { get; set; }

            public int[]? AddLineIds { get; set; }
            public int[]? RmoveLineIds { get; set; }
            public List<PhoneLine>? PhoneLines { get; set; }
            public List<PhoneLine>? SecondListPhoneLines { get; set; }
            public List<AppUser>? AppUsers { get; set; }
            public List<Department>? Departments { get; set; }
        }
        
    }
}
