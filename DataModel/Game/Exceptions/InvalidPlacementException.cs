namespace OceanBattle.DataModel.Game.Exceptions
{
    public class InvalidPlacementException : Exception
    {
        public InvalidPlacementException()
            : base()
        {
        }

        public InvalidPlacementException(string? message)
            : base(message)
        {
        }

        public InvalidPlacementException(
            string? message,
            Exception? innerException)
            : base(
                  message,
                  innerException)
        {
        }
    }
}
