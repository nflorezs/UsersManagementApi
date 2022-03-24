using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class DatumDto
    {
        public int id { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string first_name { get; set; }
        public string? last_name { get; set; }
        public string? avatar { get; set; }
    }
}