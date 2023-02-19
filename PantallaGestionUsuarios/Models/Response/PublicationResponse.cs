using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaGestionUsuarios.Models
{
    public class PublicationResponse
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
            public string name { get; set; }
            public string category { get; set; }
            public string distance { get; set; }
            public string difficulty { get; set; }
            public string duration { get; set; }
            public string description { get; set; }
            public string[] photo { get; set; }
            public string privacy { get; set; }
            public object[] rec_movement { get; set; }
            public string[] fav_routes { get; set; }
        }

        public class RootobjectSingle
        {
            public string status { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public string _id { get; set; }
            public string email { get; set; }
            public string name { get; set; }
            public string category { get; set; }
            public string distance { get; set; }
            public string difficulty { get; set; }
            public string duration { get; set; }
            public string description { get; set; }
            public string[] photo { get; set; }
            public string privacy { get; set; }
            public object[] rec_movement { get; set; }
            public string[] fav_routes { get; set; }
            public string date { get; set; }
        }
    }
}

