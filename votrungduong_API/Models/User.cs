using Microsoft.AspNetCore.Identity;

namespace votrungduong_API.Models
{
    public class User : IdentityUser
    {
        public string? Initials { get; set; }
        //User = IdentityUser + string Inititals 
    }
}
