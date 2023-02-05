using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaGestionUsuarios.Models
{
    public class CommentsResponse
    {
        public class Rootobject
        {
            public string status { get; set; }
            public Datum[] data { get; set; }
        }

        public class Datum
        {
            public string _id { get; set; }
            public string date { get; set; }
            public string message { get; set; }
            public int id_user { get; set; }
            public int id_publication { get; set; }

        }
    }
}
