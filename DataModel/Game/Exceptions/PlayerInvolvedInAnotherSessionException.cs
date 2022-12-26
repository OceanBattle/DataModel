using Microsoft.AspNetCore.SignalR;

namespace OceanBattle.DataModel.Game.Exceptions
{
    public class PlayerInvolvedInAnotherSessionException : HubException
    {
        public static readonly string Name = "PlayerInvolvedInAnotherSessionException";

        public PlayerInvolvedInAnotherSessionException()
            : base(Name)
        {
        }

        public PlayerInvolvedInAnotherSessionException(Exception? innerException)
            : base(
                  Name,
                  innerException)
        {
        }
    }
}
