using System.ComponentModel.DataAnnotations;

namespace web_app.Models
{
    public class Privilege
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
