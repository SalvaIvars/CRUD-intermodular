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
        public int id_user { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string date { get; set; }
        public string nick { get; set; }
        public string password { get; set; }
        public string photo { get; set; }
        public string rol { get; set; }

        public UserModel(string _id, int id_user, string name, string lastname, string email, string date, string nick, string password, string photo, string rol)
        {
            this._id = _id;
            this.id_user = id_user;
            this.name = name;
            this.lastname = lastname; 
            this.email = email;
            this.date = date;
            this.nick = nick;
            this.password = password;
            this.photo = photo;
            this.rol = rol;
        }

    }


}
