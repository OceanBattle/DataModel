using Microsoft.AspNetCore.SignalR;

namespace OceanBattle.DataModel.Game.Exceptions
{
    public class NotYourTurnException : HubException
    {
        public static readonly string Name = "NotYourTurnException";

        public NotYourTurnException()
            : base(Name)
        {
        }

        public NotYourTurnException(Exception? innerException)
            : base(
                  Name,
                  innerException)
        {
        }
    }
}
