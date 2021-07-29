namespace CheckInspecao.Transport.Exceptions
{
    public class UniqueException : CadastrosException
    {
        public UniqueException(string message)
        :base(message)
        {
            var msgErro = message.Split(':');
            CampoUnico = msgErro[msgErro.Length-1].Trim();
        }
        public override string ToMessage(){
            return $"O campo {CampoUnico} deve ser único";
        }
        public string CampoUnico { get; set; }
    }
}