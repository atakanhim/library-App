﻿namespace libraryApp.API.Models
{
    public class UpdateUserViewModel
    {
        public string Id { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}
