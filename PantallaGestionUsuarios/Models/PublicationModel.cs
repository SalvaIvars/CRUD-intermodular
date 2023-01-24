using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaGestionUsuarios.Models
{
    public class PublicationModel
    {
        public string _id { get; set; }
        public string fecha { get; set; }
        public string nombre { get; set; }
        public string categoria { get; set; }
        public float  distancia { get; set; }
        public string dificultad { get; set; }
        public float duracion { get; set; }
        public string descripcion { get; set; }
        public string foto { get; set; }
        public string privacidad { get; set; }
    }
}
