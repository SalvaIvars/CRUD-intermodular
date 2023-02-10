using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaGestionUsuarios.Models.Response
{
    internal class PublicationResponseNames
    {

        public class Rootobject
        {
            public string status { get; set; }
            public string[] data { get; set; }
        }

        public class Datum
        {
            public string[] namePhoto { get; set; }  

        }
    }
}
