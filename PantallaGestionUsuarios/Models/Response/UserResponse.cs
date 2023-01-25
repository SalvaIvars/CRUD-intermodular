using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaGestionUsuarios.Api
{
    public class UserResponse
    {

        public class Rootobject
        {
            public string status { get; set; }
            public Datum[] data { get; set; }
        }

        public class Datum
        {
            public string _id { get; set; }
            public int id_usuario { get; set; }
            public string nombre { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string rol { get; set; }
            public string apellidos { get; set; }
            public string fecha { get; set; }
            public string nick { get; set; }
            public string foto { get; set; }


        }

    }
}
