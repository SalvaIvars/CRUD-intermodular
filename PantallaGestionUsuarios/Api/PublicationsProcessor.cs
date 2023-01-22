using PantallaGestionUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace PantallaGestionUsuarios.Api
{
    public class PublicationsProcessor
    {
        public static async Task<PublicationModel> LoadPublication(string id = "") // Publicación de prueba
        {
            string url = "http://localhost:8080/publications/";

            if (id.Length != 0)
            {
                url += id;
            }
            else
            {
                url += "63c7a9b8ba57368e8e332ef8";
            }

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    PublicationModel publication = await response.Content.ReadAsAsync<PublicationModel>();
                    return publication;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<PublicationModel[]> LoadAllPublications()
        {
            string url = "http://localhost:8080/publications";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    PublicationModel[] publications = await response.Content.ReadAsAsync<PublicationModel[]>();
                    return publications;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        // PRUEBA
        public static async Task PostPublication()
        {
            string url = "http://localhost:8080/publications";
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    id_usuario = "212",
                    fecha = "20/01/2023",
                    nombre = "caminata por la montaña",
                    categoria = "Caminata",
                    distancia = 12.3f,
                    duracion = 1.4f,
                    descripcion = "fasdfasd fasd fasdf",
                    foto = "www.fasfsdfasdf.jpg",
                    privacidad = "private",
                    empresa = "empresa",
                    url = "url"
                }),
                Encoding.UTF8,
                "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, jsonContent))
            {
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("error");
                    MessageBox.Show(response.ReasonPhrase.ToString());
                }
            }
        }

        public static async Task UpdatePublication(string id, StringContent jsonConent)
        {
            string url = "http://localhost:8080/publications/";

            url += id;


            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync(url, jsonConent))
            {

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Update Publication");
                }
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error");
                    MessageBox.Show(response.ReasonPhrase.ToString());
                }

            }
        }

        public static async Task DeletePublication(string id = "")
        {
            string url = "http://localhost:8080/publications/";

            if (id.Length == 0)
            {
                return;
            }

            url += id;

            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("error");
                    MessageBox.Show(response.ReasonPhrase.ToString());
                }

            }
        }
    }
}
