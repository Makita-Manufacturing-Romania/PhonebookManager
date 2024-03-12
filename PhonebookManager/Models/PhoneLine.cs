namespace PhonebookManager.Models
{
    public class PhoneLine
    {
        public int Id { get; set; }
        public string? PhoneNumber { get; set; }
        public Department? Department { get; set; }
        public PhoneUser? LineOwner { get; set; }
        public List<PhoneUser>? LineUsers { get; set; }
        public List<ChangeRequest>? Changes { get; set; }
    }
}
