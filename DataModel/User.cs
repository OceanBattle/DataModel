using Microsoft.AspNetCore.Identity;
using OceanBattle.DataModel.Game.Abstractions;

namespace OceanBattle.DataModel
{
    public class User : IdentityUser
    {
        /// <summary>
        /// Vessels owned by this player.
        /// </summary>
        public IEnumerable<Ship>? OwnedVessels { get; set; }

        /// <summary>
        /// First name of user.
        /// </summary>
        public string? FirstName { get; set; }
    
        /// <summary>
        /// Last name of user.
        /// </summary>
        public string? LastName { get; set; }
    }
}