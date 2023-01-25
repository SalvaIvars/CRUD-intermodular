using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaGestionUsuarios
{
    public class UserModel
    {
        public string _id { get; set; }
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string fecha { get; set; }
        public string nick { get; set; }
        public string password { get; set; }
        public string foto { get; set; }
        public string web { get; set; }
        public string rol { get; set; } 
    }
}
