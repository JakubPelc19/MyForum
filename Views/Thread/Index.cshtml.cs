namespace MyForum.Views.Thread
{
    public class IndexModel
    {
        public int ThreadId { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
