using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MyForum.Views.Thread
{
    public class CreatePostModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Maximum length is 100, minimum length is 4")]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Content is required")]
        [StringLength(500, MinimumLength = 4, ErrorMessage = "Maximum length is 500")]
        public string Content { get; set; }
    }
}
