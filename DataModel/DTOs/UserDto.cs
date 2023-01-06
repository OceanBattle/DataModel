using OceanBattle.DataModel.Game.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.DTOs
{
    public class UserDto
    {
        /// <summary>
        /// User's e-mail address.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// User's username.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Vessels owned by user.
        /// </summary>
        public IEnumerable<Ship>? OwnedVessels { get; set; }
    }
}
