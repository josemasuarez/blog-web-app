using System.ComponentModel.DataAnnotations;

namespace web_app.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public List<Privilege>? Privileges { get; set; }
    }
}
