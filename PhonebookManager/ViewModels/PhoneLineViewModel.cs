using PhonebookManager.Models;

namespace PhonebookManager.ViewModels
{
    public class PhoneLineViewModel
    {

        public class PhoneLineVM
        {
            public int Id { get; set; }
            public string? PhoneNumber { get; set; }
            public Department? Department { get; set; }
            public int DepartmentId { get; set; }
            public PhoneUser? LineOwner { get; set; }
            public int LineOwnerId { get; set; }
            public List<PhoneUser>? LineUsers { get; set; }
            public List<ChangeRequest>? Changes { get; set; }

            public List<PhoneLine>? PhoneLines { get; set; }
            public List<AppUser>? AppUsers { get; set; }
            public List<Department>? Departments { get; set; }
        }
    }
}
