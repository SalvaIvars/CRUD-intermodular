using PantallaGestionUsuarios.Api;
using PantallaGestionUsuarios.Models.Response;
using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;
using Newtonsoft.Json;
using PantallaGestionUsuarios.Views.Error;
using Microsoft.AspNetCore.Http;
using System.Net.NetworkInformation;

namespace PantallaGestionUsuarios
{
    public class UserProcessor
    {
        public static async Task<bool> SingIn(StringContent jsonContent)
        {
            string url = "http://localhost:8080/auth/signin";    

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, jsonContent))
            {

                if (response.IsSuccessStatusCode)
                {
                    LoginResponse loginResponse = await response.Content.ReadAsAsync<LoginResponse>();
                    Application.Current.Properties["accessToken"] = loginResponse.accessToken;
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

        public static async Task<UserModel> LoadUser(string email) 
        {
            MessageBox.Show(email);
            string url = "http://localhost:8080/users/";

            url += email;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    UserResponse.Rootobject res = await response.Content.ReadAsAsync<UserResponse.Rootobject>();
                    UserModel user = JsonConvert.DeserializeObject<UserModel>(JsonConvert.SerializeObject(res.data[0]));
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
            string url = "http://localhost:8080/users";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode) {
                    UserResponse.Rootobject userResponse = await response.Content.ReadAsAsync<UserResponse.Rootobject>();
                    UserModel[] users = new UserModel[userResponse.data.Length];

                    for (int i = 0; i < users.Length; i++)
                    {
                        users[i] = JsonConvert.DeserializeObject<UserModel>(JsonConvert.SerializeObject(userResponse.data[i]));
                    }

                    return users;
                }
                else
                {
                    MessageBox.Show(response.ReasonPhrase);
                    return null;
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

        public static async Task PostPhoto(System.IO.Stream file, string mediaType, string userEmail)
        {
            string url = "http://localhost:8080/users/photo";
            
        }

        public static async Task<bool> SignUp(StringContent jsonContent)
        {
            string url = "http://localhost:8080/auth/signup";


            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, jsonContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    Application.Current.Properties["accessToken"] = await response.Content.ReadAsStringAsync();
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
            string url = "http://localhost:8080/users/";

            url += id;


            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync(url, jsonConent))
            {
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error");
                    MessageBox.Show(response.ReasonPhrase.ToString());
                }

            }
        }


        public static async Task DeleteUser(string id="")
        {
            string url = "http://localhost:8080/users/";

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
