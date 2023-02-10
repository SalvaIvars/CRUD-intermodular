using PantallaGestionUsuarios.Api;
using PantallaGestionUsuarios.Models.Response;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using MimeKit;
using System.Windows.Controls;
using System.IO;

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

        public static async Task PostPhoto(Image img, string fileName, string userEmail)
        {
            string url = "http://localhost:8080/users/"+userEmail;
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            var fileStreamContent = new StreamContent(File.OpenRead(new Uri(img.Source.ToString()).AbsolutePath));
            string extension = MimeTypes.GetMimeType(fileName);

            MultipartFormDataContent file = new MultipartFormDataContent();
            file.Headers.ContentType.MediaType = "multipart/form-data";
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(extension);
            file.Add(fileStreamContent, "photo", fileName);

            ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["accessToken"].ToString());
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, file))
            {
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("error");
                    MessageBox.Show(response.ReasonPhrase.ToString());
                    MessageBox.Show(response.StatusCode.ToString());
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
                    MessageBox.Show(response.Content.ToString());
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
