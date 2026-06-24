namespace SignalRSample.Modelos
{
    public class mdl_Obtener_Chat_Mensajes
    {
        public int idMensaje { get; set; }
        public string numeroTelefono { get; set; }
        public string mensaje { get; set; }
        public string mensajePlantilla { get; set; }
        public string? archivo { get; set; }
        public string? extension { get; set; }
        public string estatus { get; set; }
        public int createuser { get; set; }
        public string origino { get; set; }
        public string empleadoEnviado { get; set; }
        public string fecha { get; set; }
    }
}
