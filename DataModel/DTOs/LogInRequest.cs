using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.DTOs
{
    public class LogInRequest
    {
        /// <summary>
        /// User's password.
        /// </summary>
        public string? Password { get; set; }
        
        /// <summary>
        /// User's email.
        /// </summary>
        public string? Email { get; set; }
    }
}
