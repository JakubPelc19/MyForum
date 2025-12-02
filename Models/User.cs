using System.ComponentModel.DataAnnotations;

public class User
{
    public Guid Id { get; set; }
    
    public string Username { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<ThreadForum> Threads { get; } = new List<ThreadForum>();
    public ICollection<Post> Posts { get; } = new List<Post>();
    public ICollection<Comment> Comments { get; } = new List<Comment>();
}