using Microsoft.AspNetCore.SignalR;

namespace OceanBattle.DataModel.Game.Exceptions
{
    public class ShipNotAvailableException : HubException
    {
        public static readonly string Name = "ShipNotAvailableException";

        public ShipNotAvailableException()
            : base(Name)
        {
        }

        public ShipNotAvailableException(Exception? innerException)
            : base(
                  Name,
                  innerException)
        {
        }
    }
}
