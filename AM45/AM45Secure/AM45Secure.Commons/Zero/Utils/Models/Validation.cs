namespace Zero.Utils.Models
{
    public class Validation
    {
        public string IdField { get; set; } //nombre del campo al que se le aplica la validacion
        public string Field { get; set; }//nombre del campo que esta contenido en el properties
        public string IdMessage { get; set; }//clave del mensaje de error que se encuentra en el properties
        public string Message { get; set; }//mensaje del properties.

        public Validation()
        {
        }

        public Validation(string message)
        {
            Message = message;
        }

        public Validation(
            string message,
            string idMessage)
        {
            Message = message;
            IdMessage = idMessage;
        }
    }
}
