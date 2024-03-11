﻿namespace PhonebookManager.Models
{
    public class Employee
    {
        public string? EmployeeID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? FullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartDateHR { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime MapEndDate { get; set; }
        public string? Department { get; set; }
        public string? Title { get; set; }
        public string? Activity { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Manager { get; set; }
        public string? CurrentRank { get; set; }
        public string? LongPass { get; set; } = string.Empty;
        public string? ShortPass { get; set; } = string.Empty;
        public decimal ErrorCount { get; set; }
    }
}
