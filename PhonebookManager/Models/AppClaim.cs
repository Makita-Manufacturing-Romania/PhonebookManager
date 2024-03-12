namespace PhonebookManager.Models
{
    public class AppClaim
    {
        public int Id { get; set; }
        public string? Claim { get; set; }
        public List<AppRole>? Roles { get; set; }
    }
}

