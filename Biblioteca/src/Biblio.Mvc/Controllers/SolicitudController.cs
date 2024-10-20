using Biblio.Core;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Mvc.Controllers;

public class SolicitudController:Controller
{
    protected readonly IAdo Ado;
    public SolicitudController(IAdo ado) => Ado = ado;
    
}
