public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public string Content { get; set; } = string.Empty;
    public int Likes { get; set; }
}