using Microsoft.AspNetCore.SignalR;

namespace OceanBattle.DataModel.Game.Exceptions
{
    public class InvalidHitException : HubException
    {
        public static readonly string Name = "InvalidHitException";

        public InvalidHitException()
            : base(Name)
        {
        }

        public InvalidHitException(Exception? innerException)
            : base(
                  Name,
                  innerException)
        {
        }
    }
}
