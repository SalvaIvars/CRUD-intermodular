﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaGestionUsuarios.Models.Response
{
    internal class LoginResponse
    {
        public string status { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
