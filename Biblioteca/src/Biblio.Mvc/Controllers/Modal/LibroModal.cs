﻿using Biblio.Core;

namespace Biblio.Mvc.Controllers.Modal
{
    public class LibroModal
    {
        public ulong ISBN { get; set; }
        public uint idTitulo { get; set; }
        public uint idEditorial { get; set; }
        public string busqueda { get; set; }
        public List<Titulo> titulos { get; set; } = new List<Titulo>();
        public List<Editorial> editoriales { get; set; } = new List<Editorial>();
        public List<Libro>libros{ get; set; } = new List<Libro>();
        public LibroModal() {}
    }
}
