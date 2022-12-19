namespace OceanBattle.DataModel.Game.Exceptions
{
    public class NotYourTurnException : Exception
    {
        public NotYourTurnException()
            : base()
        {
        }

        public NotYourTurnException(string? message)
            : base(message)
        {
        }

        public NotYourTurnException(
            string? message,
            Exception? innerException)
            : base(
                  message,
                  innerException)
        {
        }
    }
}
