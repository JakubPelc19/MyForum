using Microsoft.AspNetCore.Identity;

namespace MyForum.Models
{
    public class User : IdentityUser
    {
        public ICollection<ThreadForum> Threads { get; } = new List<ThreadForum>();
        public ICollection<Post> Posts { get; } = new List<Post>();
        public ICollection<Comment> Comments { get; } = new List<Comment>();

    }
}
