using MyForum.Models;

public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public string AuthorId { get; set; }
    public User Author { get; set; } = null!;
    public string Content { get; set; }
    public int Likes { get; set; }
}