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
        public string date { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string  distance { get; set; }
        public string difficulty { get; set; }
        public string duration { get; set; }
        public string description { get; set; }
        public string[] photo { get; set; }
        public string privacy { get; set; }
        public object[] rec_movement { get; set; }

        public PublicationModel(string _id, string date, string name, string category, string distance, string difficulty, string duration, string description, string[] photo, string privacy, object[] rec_movement) { 
            this._id = _id;
            this.date = date;
            this.name = name;
            this.category = category;
            this.distance = distance;
            this.difficulty = difficulty;
            this.duration = duration;
            this.description = description;
            this.photo = photo;
            this.privacy = privacy;
            this.rec_movement = rec_movement;
        }
    }

}
