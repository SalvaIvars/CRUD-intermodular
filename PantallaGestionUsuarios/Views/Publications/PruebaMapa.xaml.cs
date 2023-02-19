using System.Text.Json;
using Newtonsoft.Json;
using PantallaGestionUsuarios.Models;
using System.Collections.Generic;
using System.Windows;
using Newtonsoft.Json.Linq;
using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System;
using System.IO;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq;

namespace PantallaGestionUsuarios.Views.Publications
{
    /// <summary>
    /// Lógica de interacción para PruebaMapa.xaml
    /// </summary>
    public partial class PruebaMapa : Window
    {
        public class Rootobject
        {
            public float latitude { get; set; }
            public float longitude { get; set; }
        }

        public object[] rec_movement;
        public PruebaMapa(object[] movement)
        {
            InitializeComponent();
            this.rec_movement = movement;
            Wb.EnsureCoreWebView2Async();

        }

        private void minimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void closeWindow(object sender, RoutedEventArgs e)
        {
            RemoveRoute();
            this.Close();
        }

        private void EditScript(string jsonLista)
        {
            File.AppendAllText(@"C:/Users/Salva/source/repos/SalvaIvars/CRUDPorfavor/PantallaGestionUsuarios/bin/Debug/net6.0-windows/script.js", "var flightPlanCoordinates = " + jsonLista + "\r\nvar prueba = [];\r\nfor(let i = 0;  i < flightPlanCoordinates.length; i++){\r\n  prueba[i] = new google.maps.LatLng(flightPlanCoordinates[i][0], flightPlanCoordinates[i][1])\r\n}\r\n loadPosition(prueba[0])\r\nvar flightPath = new google.maps.Polyline({\r\n    map:map,\r\n    path: prueba,\r\n    strokeColor: \"#FF0000\",\r\n    strokeOpacity: 1.0,\r\n    strokeWeight: 2\r\n  })}" + Environment.NewLine);
        }

        private async void LoadMap(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
             LoadLatLng();

            Wb.CoreWebView2.Navigate("file:///C:/Users/Salva/source/repos/SalvaIvars/CRUDPorfavor/PantallaGestionUsuarios/bin/Debug/net6.0-windows/index.html");

        }

        private async void RemoveRoute()
        {
            List<string> quotelist = File.ReadAllLines(@"C:/Users/Salva/source/repos/SalvaIvars/CRUDPorfavor/PantallaGestionUsuarios/bin/Debug/net6.0-windows/script.js").ToList();
            for(int i = 36; i > 23; i--)
            {
                string firstItem = quotelist[i];
                quotelist.RemoveAt(i);
            }

            File.WriteAllLines(@"C:/Users/Salva/source/repos/SalvaIvars/CRUDPorfavor/PantallaGestionUsuarios/bin/Debug/net6.0-windows/script.js", quotelist.ToArray());
        }

        private async void LoadLatLng()
        {
            List<List<double>> lista = new List<List<double>>();

            for(int i = 0; i < rec_movement.Length; i++)
            {
                string str = rec_movement[i].ToString();
                JObject json = JObject.Parse(str);
                List<double> listDouble = new List<double>();
                foreach (var j in json)
                {
                    listDouble.Add((double)j.Value);
                }   
                lista.Add(listDouble);
            }

            var jsonLista = JsonConvert.SerializeObject(lista);
            EditScript(jsonLista.ToString());
        }
    }
}



