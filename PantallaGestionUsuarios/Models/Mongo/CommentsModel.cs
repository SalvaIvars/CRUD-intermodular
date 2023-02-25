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
        public string date { get; set; }
        public string message { get; set; }
        public string email { get; set; }
        public string id_publication { get; set; }

    }
}
