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
            public string name { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public string date { get; set; }
            public string nick { get; set; }
            public string password { get; set; }
            public string photo { get; set; }
            public string rol { get; set; }

        }

    }
}
