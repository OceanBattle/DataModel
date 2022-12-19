namespace OceanBattle.DataModel.Game.Exceptions
{
    public class SessionInactiveException : Exception
    {
        public SessionInactiveException()
            : base()
        {
        }

        public SessionInactiveException(string? message)
            : base(message)
        {
        }

        public SessionInactiveException(
            string? message,
            Exception? innerException)
            : base(
                  message,
                  innerException)
        {
        }
    }
}
