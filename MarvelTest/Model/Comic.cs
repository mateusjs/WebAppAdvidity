using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarvelTest.Model
{
    public class Comic
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string UrlImagem { get; set; }
        public string CopyRights { get; set; }
        public string Attribution { get; set; }
    }
}