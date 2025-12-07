using MyForum.Models;

public class Post
{
    public int Id { get; set; }
    public int ThreadId { get; set; }
    public ThreadForum ThreadForum { get; set; } = null!;
    public string AuthorId { get; set; }
    public User Author { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Likes { get; set; }
    public ICollection<Comment> Comments { get; } = new List<Comment>();
    
}