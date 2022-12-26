using Microsoft.AspNetCore.SignalR;

namespace OceanBattle.DataModel.Game.Exceptions
{
    public class NotAllShipsPlacedException : HubException
    {
        public static readonly string Name = "NotAllShipsPlacedException"; 

        public NotAllShipsPlacedException() 
            : base(Name) 
        {
        }

        public NotAllShipsPlacedException(Exception? innerException) 
            : base(
                  Name, 
                  innerException) 
        {  
        }
    }
}
