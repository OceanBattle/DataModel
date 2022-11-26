using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel
{
    /// <summary>
    /// Base class for all databse models.
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Model key.
        /// </summary>
        public Guid Id { get; set; }
    }
}
