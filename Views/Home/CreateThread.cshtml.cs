using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MyForum.Views.Home
{
    public class CreateThreadModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Maximum length is 40, minimum length is 4")]
        public string Title { get; set; }
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Maximum length is 100, minimum length is 4")]
        public string? Description { get; set; }

    }
}
