namespace Tahseen.Service.Exceptions
{
    public class TahseenException : Exception
    {
        public int statusCode;
        public TahseenException(int code, string message) : base(message) 
        {
            this.statusCode = code;
        }
    }
}
