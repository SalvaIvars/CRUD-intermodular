using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaGestionUsuarios.Models
{
    public class CommentsModel
    {
        public string _id { get; set; }
        public string fecha { get; set; }
        public string mensaje { get; set; }
        public string id_usuario { get; set; }
        public string id_publicacion { get; set; }
        public string descripcion { get; set; }

    }
}
