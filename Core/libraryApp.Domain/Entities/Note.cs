using libraryApp.Domain.Entities.Common;
using libraryApp.Domain.Entities.Identity;

namespace libraryApp.Domain.Entities
{
    public class Note:BaseEntity
    {
 
        public string Content { get; set; }
        public PrivacySettings PrivacySettings { get; set; } // Gizlilik ayarı
        public string BookId { get; set; }
        public Book Book { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
 
}