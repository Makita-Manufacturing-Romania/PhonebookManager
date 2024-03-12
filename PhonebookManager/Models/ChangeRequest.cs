namespace PhonebookManager.Models
{
    public class ChangeRequest
    {
        public int Id { get; set; }
        public PhoneUser? OldName { get; set; }
        public PhoneUser? NewName { get; set; }
        public DateTime? RequestDate { get; set; } = default(DateTime?);
        public AppUser? RequesterId { get; set; }
        public AppUser? ItOperator { get; set; }
        public DateTime? ImplementationDate { get; set; } = default(DateTime?);
        public PhoneLine? PhoneLine { get; set; }
        public string? Status { get; set; }

    }
}
