using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks_Api.Models.DTO
{
    public class AddUserRequest
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; }
    }
}
