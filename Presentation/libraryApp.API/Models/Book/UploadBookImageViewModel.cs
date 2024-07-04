namespace libraryApp.API.Models.Book
{
    public class UploadBookImageViewModel
    {
        public string Id { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
