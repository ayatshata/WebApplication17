using System.ComponentModel.DataAnnotations;

namespace WebApplication17.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Manager { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Location { get; set; } = string.Empty;

        [Range(0, 1000)]
        public int NumOfStd { get; set; }
    }
}
