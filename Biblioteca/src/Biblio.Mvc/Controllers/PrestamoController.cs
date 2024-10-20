using Biblio.Core;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Mvc.Controllers;

public class PrestamoController:Controller
{
    protected readonly IAdo Ado;
    public PrestamoController(IAdo ado) => Ado = ado;
    

}
