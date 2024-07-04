using Microsoft.AspNetCore.Identity;

namespace libraryApp.Domain.Entities.Identity
{
    public class AppUser: IdentityUser<string>
    {
        public string? FullName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ICollection<Note> Notes { get; set; } = new List<Note>();

    }
}
