using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class DatumLoginDto
    {
        public int id { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string first_name { get; set; }
        public string? last_name { get; set; }
        public string? avatar { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}