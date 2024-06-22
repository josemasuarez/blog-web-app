using System.ComponentModel.DataAnnotations;

namespace web_app.Models
{
    public class BlogArticle
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }
                
        [Display(Name = "Published")]
        public bool IsPublished { get; set; }
    }
}
