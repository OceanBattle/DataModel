using Microsoft.AspNetCore.SignalR;

namespace OceanBattle.DataModel.Game.Exceptions
{
    public class InvalidPlacementException : HubException
    {
        public static readonly string Name = "InvalidPlacementException";

        public InvalidPlacementException()
            : base(Name)
        {
        }

        public InvalidPlacementException(Exception? innerException)
            : base(
                  Name,
                  innerException)
        {
        }
    }
}
