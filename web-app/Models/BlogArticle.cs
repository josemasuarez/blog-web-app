using System.ComponentModel.DataAnnotations;

namespace web_app.Models
{
    public class BlogArticle
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Titulo")]
        public required string Title { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        public required string Description { get; set; }

        [Display(Name = "Fecha de creacion")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Fecha de actualizacion")]
        public DateTime UpdatedDate { get; set; }
                
        [Display(Name = "Publicado")]
        public bool IsPublished { get; set; }

        [Required]
        [Display(Name = "Autor")]
        public required string Author { get; set; }
    }
}
