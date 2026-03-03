using System.Collections.Generic;

namespace SignalRSample.Modelos
{
    public class mdl_Notificaciones_recepcion
    {
        public string? numeroTelefono { get; set; }
        public string? cliente { get; set; }
        public string? mensaje { get; set; }
        public List<string>? usuarios { get; set; }
    }
}
