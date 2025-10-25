using System.ComponentModel.DataAnnotations;

namespace WebApplication17.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Dept { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Manager { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [MaxLength(200)]
        public string Skills { get; set; } = string.Empty;
    }
}
