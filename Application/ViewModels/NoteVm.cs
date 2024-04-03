namespace Application.ViewModels
{
    public class NoteVM
    {
        public Guid Id { get; set; }
        public string Preview { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
