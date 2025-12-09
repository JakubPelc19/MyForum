using MyForum.Models;

public class ThreadForum
{
    public int Id { get; set; }
    public string AuthorId { get; set; }
    public User Author { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<Post> Posts { get; } = new List<Post>();

}