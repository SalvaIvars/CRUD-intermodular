using Newtonsoft.Json;
using PantallaGestionUsuarios.Api;
using PantallaGestionUsuarios.Models.Response;
using PantallaGestionUsuarios.Views.Error;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

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
                    string error = " a";

                    //PersonalError e = new PersonalError(error);
                    //e.ShowDialog();
                    return true;
                }
                else
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    //PersonalErrorMessage er = new PersonalErrorMessage(error);
                    MessageBox.Show(error);
                    return false;
                }
            }
        }

        public static async Task<UserModel> LoadUser(string id="") 
        {
            string url = "http://localhost:8080/users/";

            url += id;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    UserResponse.Datum res = await response.Content.ReadAsAsync<UserResponse.Datum>();
                    MessageBox.Show("RES: " + res.name);
                    UserModel user = new UserModel(res._id, res.id_user, res.name, res.email, res.password, res.rol, res.lastname, res.date, res.nick, res.photo);
                    MessageBox.Show("user: " + user.name);
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
