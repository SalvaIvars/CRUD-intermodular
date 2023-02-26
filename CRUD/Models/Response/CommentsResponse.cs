using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Models
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
            public string email { get; set; }
            public string id_publication { get; set; }

        }
    }
}
