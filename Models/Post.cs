using MyForum.Models;

public class Post
{
    public int Id { get; set; }
    public int ThreadForumId { get; set; }
    public ThreadForum ThreadForum { get; set; } = null!;
    public string AuthorId { get; set; }
    public User Author { get; set; } = null!;
    public string Title { get; set; }
    public string Content { get; set; }
    public int Likes { get; set; }
    public ICollection<Comment> Comments { get; } = new List<Comment>();
    
}