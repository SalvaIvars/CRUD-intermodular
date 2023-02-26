using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    public class UserModel
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
        public string[] following { get; set; }
        public string[] fav_routes { get; set; }
        public string description { get; set; }

        public UserModel(string _id, string name, string lastname, string email, string date, string nick, string password, string photo, string rol, string[] following, string[] fav_routes, string description)
        {
            this._id = _id;
            this.name = name;
            this.lastname = lastname; 
            this.email = email;
            this.date = date;
            this.nick = nick;
            this.password = password;
            this.photo = photo;
            this.rol = rol;
            this.following = following;
            this.fav_routes = fav_routes;
            this.description = description;
        }

    }


}
