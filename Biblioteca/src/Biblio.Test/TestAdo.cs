using Biblio.Core;

namespace Biblio.Test;

public class TestAdo
{
    protected readonly IAdo Ado;
    private static readonly string _cadena =
        @"Server=localhost;Database=5to_Biblioteca;Uid=root;pwd=Jose.jose1;Allow User Variables=True";
    public TestAdo() => Ado = new Biblio.AdoDapper.AdoDapper(_cadena);
    public TestAdo(string cadena) => Ado = new Biblio.AdoDapper.AdoDapper(cadena);
}