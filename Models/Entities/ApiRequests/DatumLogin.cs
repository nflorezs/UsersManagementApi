using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class DatumLogin
    {
        public int id { get; set; }
        public string? email { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? avatar { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}