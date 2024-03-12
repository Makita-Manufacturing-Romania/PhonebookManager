namespace PhonebookManager.Models
{
    public class AppClaim
    {
        public int Id { get; set; }
        public System.Security.Claims.Claim? Claim { get; set; }
        public List<AppRole>? Roles { get; set; }
    }
}
