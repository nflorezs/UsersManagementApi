using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class AuthenticateResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponseDto(DatumLoginDto user, string token)
        {
            Id = user.id;
            FirstName = user.first_name;
            LastName = user.last_name;
            Username = user.Username;
            Token = token;
        }
    }
}
