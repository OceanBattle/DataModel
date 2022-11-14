using Microsoft.AspNetCore.Identity;

namespace DataModel
{
    public class User : IdentityUser
    {
        /// <summary>
        /// First name of user.
        /// </summary>
        public string? FirstName { get; set; }
    
        /// <summary>
        /// Last name of user.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// User's date of birth.
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}