using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aarco.Models
{
    public class Sepomex
    {
        public Municipio Municipio { set; get; }
        
        public List<Ubicacion> Ubicacion { set; get; }
    }

    public class Municipio
    {
        public string sMunicipio { set; get; }
        public Estado Estado { set; get; }
    }

    public class Estado
    {
        public string sEstado { set; get; }
    }
    public class Ubicacion
    {
        public string sUbicacion { set; get; }
    }
}
