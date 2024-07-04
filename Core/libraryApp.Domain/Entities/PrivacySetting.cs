using libraryApp.Domain.Entities;
using libraryApp.Domain.Entities.Common;

namespace libraryApp.Domain.Entities
{
    public class PrivacySettings : BaseEntity
    {
        private bool _isPublic;
        private bool _isFriendsOnly;
        private bool _isPrivate;

        public bool IsPublic
        {
            get => _isPublic;
            set
            {
                if (value)
                {
                    _isFriendsOnly = false;
                    _isPrivate = false;
                }
                _isPublic = value;
            }
        }

        public bool IsFriendsOnly
        {
            get => _isFriendsOnly;
            set
            {
                if (value)
                {
                    _isPublic = false;
                    _isPrivate = false;
                }
                _isFriendsOnly = value;
            }
        }

        public bool IsPrivate
        {
            get => _isPrivate;
            set
            {
                if (value)
                {
                    _isPublic = false;
                    _isFriendsOnly = false;
                }
                _isPrivate = value;
            }
        }
        public string NoteId { get; set; }
        public Note Note { get; set; }

    }
}
