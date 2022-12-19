namespace OceanBattle.DataModel.Game.Exceptions
{
    public class OponentNotFoundException : Exception
    {
        public OponentNotFoundException()
            : base()
        {
        }

        public OponentNotFoundException(string? message)
            : base(message)
        {
        }

        public OponentNotFoundException(
            string? message,
            Exception? innerException)
            : base(
                  message,
                  innerException)
        {
        }
    }
}
