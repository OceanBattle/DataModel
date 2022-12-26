using Microsoft.AspNetCore.SignalR;

namespace OceanBattle.DataModel.Game.Exceptions
{
    public class OponentNotFoundException : HubException
    {
        public static readonly string Name = "OponentNotFoundException";

        public OponentNotFoundException()
            : base(Name)
        {
        }

        public OponentNotFoundException(Exception? innerException)
            : base(
                  Name,
                  innerException)
        {
        }
    }
}
