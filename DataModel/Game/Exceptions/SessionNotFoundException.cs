using Microsoft.AspNetCore.SignalR;

namespace OceanBattle.DataModel.Game.Exceptions
{
    public class SessionNotFoundException : HubException
    {
        public static readonly string Name = "SessionNotFoundException";

        public SessionNotFoundException()
            : base(Name)
        {
        }

        public SessionNotFoundException(Exception? innerException)
            : base(
                  Name,
                  innerException)
        {
        }
    }
}
