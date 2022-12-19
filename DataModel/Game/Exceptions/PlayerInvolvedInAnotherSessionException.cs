using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.Game.Exceptions
{
    public class PlayerInvolvedInAnotherSessionException : Exception
    {
        public PlayerInvolvedInAnotherSessionException()
            : base()
        {
        }

        public PlayerInvolvedInAnotherSessionException(string? message)
            : base(message)
        {
        }

        public PlayerInvolvedInAnotherSessionException(
            string? message,
            Exception? innerException)
            : base(
                  message,
                  innerException)
        {
        }
    }
}
