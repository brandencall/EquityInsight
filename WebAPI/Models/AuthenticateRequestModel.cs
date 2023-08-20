using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class AuthenticateRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
