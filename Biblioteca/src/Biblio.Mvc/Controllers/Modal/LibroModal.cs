using Biblio.Core;

namespace Biblio.Mvc.Controllers.Modal
{
    public class LibroModal
    {

        public Titulo Titulo { get; set; }
        public Editorial Editorial { get; set; }
        public ulong ISBN { get; set; }

        //List<Solicitud> Solicitudes { get; set; }
        //List<FueraCirculacion> FueraCirculaciones { get; set; }
        //List<Prestamo> Prestamos { get; set; }
        public LibroModal()
        {
        }

    }
}
