namespace Restaurant.Domain.Exepections
{
    public class NotFoundExepection : Exception
    {
        #region Constructor
        private readonly string message;
        public NotFoundExepection(string Message)
        {
            message = Message;
        }
        #endregion
    }
}
