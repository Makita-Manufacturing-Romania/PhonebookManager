﻿namespace PhonebookManager.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public AppUser? Manager { get; set; }
        public AppUser? Responsible { get; set; }
        public List<PhoneLine>? Lines { get; set; }
    }
}
