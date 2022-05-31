using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Display(Name = "Display Category")]
        [Range(1, 10000, ErrorMessage = "Max input field is 4!!")]
        public int DisplayOrder { get; set; }
    }

}