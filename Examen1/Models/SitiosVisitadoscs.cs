using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examen1.Models
{
    public class SitiosVisitadoscs
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(100)]
        public string Sitio { get; set; }

        public byte[] FotoLugar { get; set; }
       
        [MaxLength(100)]
        public string Pais { get; set; }

        [MaxLength(100)]
        public string Nota { get; set; }

        public double latitud { get; set; }
        public double longitud { get; set; }

    }
}
