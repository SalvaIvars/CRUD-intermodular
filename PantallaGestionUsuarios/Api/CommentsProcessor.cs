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
    public class CommentsProcessor
    {
        public static async Task<CommentsModel> LoadComment(string id = "") // Publicación de prueba
        {
            string url = "http://localhost:8080/comments/";

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
                    CommentsModel comment = await response.Content.ReadAsAsync<CommentsModel>();
                    return comment;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<CommentsModel[]> LoadAllComments()
        {
            string url = "http://localhost:8080/comments";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    CommentsModel[] comments = await response.Content.ReadAsAsync<CommentsModel[]>();
                    return comments;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        // PRUEBA
        public static async Task PostComment()
        {
            string url = "http://localhost:8080/comments";
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    fecha = "22/02/2022",
                    mensaje = "mensaje",
                    id_usuario = "1",
                    id_publicacion = "2"
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


        public static async Task DeleteComment(string id = "")
        {
            string url = "http://localhost:8080/comments/";

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
