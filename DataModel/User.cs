using Microsoft.AspNetCore.Identity;
using OceanBattle.DataModel.Game.Abstractions;

namespace OceanBattle.DataModel
{
    public class User : IdentityUser
    {
        //public IEnumerable<Ship>? OwnedVessels { get; set; }

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