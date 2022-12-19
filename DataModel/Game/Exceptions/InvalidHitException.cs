namespace OceanBattle.DataModel.Game.Exceptions
{
    public class InvalidHitException : Exception
    {
        public InvalidHitException()
            : base()
        {
        }

        public InvalidHitException(string? message)
            : base(message)
        {
        }

        public InvalidHitException(
            string? message,
            Exception? innerException)
            : base(
                  message,
                  innerException)
        {
        }
    }
}
