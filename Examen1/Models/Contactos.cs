using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Examen1.Models
{
    public class Contactos
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Apellidos { get; set; }

        public int Edad { get; set; }

        [MaxLength(100)]
        public string Pais { get; set; }

        [MaxLength(100)]
        public string Nota { get; set; }

        public double latitud { get; set; }
        public double longitud { get; set;}

    }
}
