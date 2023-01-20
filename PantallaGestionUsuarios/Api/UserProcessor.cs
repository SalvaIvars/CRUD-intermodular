using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace PantallaGestionUsuarios
{
    public class UserProcessor
    {

        private static string token { get; set; }

        public static string ObtainToken()
        {
            return token;
        }
        public static async Task<bool> SingIn(string username, string password)
        {
            string url = "http://localhost:8080/auth/signin";

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    nombre = username.TrimEnd(),
                    password = password.TrimEnd(),
                }),
                Encoding.UTF8,
                "application/json");


            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, jsonContent))
            {

                if (response.IsSuccessStatusCode)
                {
                    token = await response.Content.ReadAsStringAsync();
                    return true;
                }
                else
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show(error);
                    return false;
                }
            }
        }
        public static async Task<UserModel> LoadUser(string id="") // Usuario de prueba
        {
            string url = "http://localhost:8080/usuarios/";

            if (id.Length != 0)
            {
                url += id;
            }
            else
            {
                url += "63c8fcc76adacd7589776a74";
            }

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    UserModel user = await response.Content.ReadAsAsync<UserModel>();
                    return user;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<UserModel[]> LoadAllUsers()
        {
            string url = "http://localhost:8080/usuarios";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode) {

                    UserModel[] users = await response.Content.ReadAsAsync<UserModel[]>();
                    return users;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        // PRUEBA
        public static async Task PostUser()
        {
            string url = "http://localhost:8080/auth/signup";
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    id_usuario = "21",
                    nombre="postc#2",
                    email="emailc#",
                    password="postc#"
                }),
                Encoding.UTF8,
                "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, jsonContent))
            {
                if (! response.IsSuccessStatusCode)
                {
                    MessageBox.Show("error");
                    MessageBox.Show(response.ReasonPhrase.ToString());
                }
            }
        }


        public static async Task DeleteUser(string id="")
        {
            string url = "http://localhost:8080/usuarios/";

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
