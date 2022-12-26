using Microsoft.AspNetCore.SignalR;

namespace OceanBattle.DataModel.Game.Exceptions
{
    public class SessionInactiveException : HubException
    {
        public static readonly string Name = "SessionInactiveException";

        public SessionInactiveException()
            : base(Name)
        {
        }

        public SessionInactiveException(Exception? innerException)
            : base(
                  Name,
                  innerException)
        {
        }
    }
}
