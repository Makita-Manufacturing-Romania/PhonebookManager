using PhonebookManager.Models;

namespace PhonebookManager.ViewModels
{
    public class DashboardViewModel
    {
        public class DashboardDepartmentVM
        {
            public int Id { get; set; }
            public string? Code { get; set; }
            public string? Name { get; set; }
            public AppUser? Manager { get; set; }
            public int ManagerId { get; set; }

            public AppUser? Responsible { get; set; }
            public int ResponsibleId { get; set; }
            public List<PhoneLine>? Lines { get; set; }

            public List<Department>? DepartmentList { get; set; }

        }
    }
}
