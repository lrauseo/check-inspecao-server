namespace CheckInspecao.Transport.Exceptions
{
    public class CadastrosException : System.Exception
    {
        public CadastrosException(string message)
        : base(message)
        {

        }
        public virtual string ToMessage()
        {
            var msg = InnerException == null? Message : InnerException.Message;
            return msg;
        }
    }
}