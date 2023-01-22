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
        public static async Task<UserModel> LoadUser(string id="") 
        {
            string url = "http://localhost:8080/usuarios/";

            url += id;

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

        public static async Task PostUser(StringContent jsonContent)
        {
            string url = "http://localhost:8080/auth/signup";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, jsonContent))
            {
                if (! response.IsSuccessStatusCode)
                {
                    MessageBox.Show("error");
                    MessageBox.Show(response.ReasonPhrase.ToString());
                }
            }
        }

        public static async Task<bool> SignUp(StringContent jsonContent)
        {
            string url = "http://localhost:8080/auth/signup";


            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, jsonContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    token = await response.Content.ReadAsStringAsync();
                    return true;
                }
                else 
                {
                    MessageBox.Show("error");
                    MessageBox.Show(response.ReasonPhrase.ToString());
                    MessageBox.Show(response.RequestMessage.ToString());
                    return false;
                }
            }
        }

        public static async Task UpdateUser(string id, StringContent jsonConent)
        {
            string url = "http://localhost:8080/usuarios/";

            url += id;


            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync(url, jsonConent))
            {

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Update User");
                }
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error");
                    MessageBox.Show(response.ReasonPhrase.ToString());
                }

            }
        }


        public static async Task DeleteUser(string id="")
        {
            string url = "http://localhost:8080/usuarios/";

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
