public class ThreadForum
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Post> Posts { get; set; } = new List<Post>();

}