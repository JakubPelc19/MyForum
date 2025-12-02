public class ThreadForum
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
    public User Author { get; set; } = null!;
    public ICollection<Post> Posts { get; } = new List<Post>();

}