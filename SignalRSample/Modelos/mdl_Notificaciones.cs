namespace SignalRSample.Modelos
{
    public class mdl_Notificaciones
    {
        public string? numeroTelefono { get; set; }
        public string? cliente { get; set; }
        public string? mensaje { get; set; }
        public List<mdl_Notificaciones_Usuarios>? usuarios { get; set; }

    }
    public class mdl_Notificaciones_Usuarios
    {
        public int usuario { get; set; }
        public int idmensaje { get; set; }
    }
}
