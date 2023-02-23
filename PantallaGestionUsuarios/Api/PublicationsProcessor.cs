using Newtonsoft.Json;
using PantallaGestionUsuarios.Models;
using PantallaGestionUsuarios.Models.Response;
using PantallaGestionUsuarios.Views.Error;
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

        public static async Task<PublicationModel[]> GetUserPublications(string email)
        {
            string url = "http://localhost:8080/publications/user/" + email;

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    PublicationResponse.Rootobject publicationResponse = await response.Content.ReadAsAsync<PublicationResponse.Rootobject>();
                    PublicationModel[] publications = new PublicationModel[publicationResponse.data.Length];
                    for (int i = 0; i < publications.Length; i++)
                    {
                        publications[i] = JsonConvert.DeserializeObject<PublicationModel>(JsonConvert.SerializeObject(publicationResponse.data[i]));
                    }

                    return publications;
                }
                else
                {
                    new CustomError(response.ReasonPhrase).ShowDialog();
                    return null;
                }
            }
        }


        public static async Task<PublicationModel> LoadPublication(string id = "")
        {
            string url = "http://localhost:8080/publications/";

            url += id;

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    PublicationResponse.Rootobject publicationResponse = await response.Content.ReadAsAsync<PublicationResponse.Rootobject>();
                    PublicationModel publicationReturn = new PublicationModel(publicationResponse.data[0]._id, publicationResponse.data[0].date, publicationResponse.data[0].name, publicationResponse.data[0].category, publicationResponse.data[0].distance, publicationResponse.data[0].difficulty, publicationResponse.data[0].duration, publicationResponse.data[0].description, publicationResponse.data[0].photo, publicationResponse.data[0].privacy, publicationResponse.data[0].rec_movement, publicationResponse.data[0].fav_routes);
                    return publicationReturn;
                }
                else
                {
                    new CustomError(response.ReasonPhrase).ShowDialog();
                    return null;
                }
            }
        }

        public static async Task<string[]> GetNumberOfPhotos(string id)
        {
            string url = "http://localhost:8080/publications/photos/";
            url += id;

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    PublicationResponseNames.Rootobject publication = await response.Content.ReadAsAsync<PublicationResponseNames.Rootobject>();
                    string[] photos = new string[publication.data.Length];

                    for (int i = 0; i < publication.data.Length; i++)
                    {
                        photos[i] = publication.data[i];

                    }
                    return photos;
                }
                else
                {
                    return null;
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
                    PublicationResponse.Rootobject publicationResponse = await response.Content.ReadAsAsync<PublicationResponse.Rootobject>();
                    PublicationModel[] publications = new PublicationModel[publicationResponse.data.Length];
                    for (int i = 0; i < publications.Length; i++)
                    {
                        publications[i] = JsonConvert.DeserializeObject<PublicationModel>(JsonConvert.SerializeObject(publicationResponse.data[i]));
                    }

                    return publications;
                }
                else
                {
                    new CustomError(response.ReasonPhrase).ShowDialog();
                    return null;
                }
            }
        }


        public static async Task PostPublication(StringContent jsonContent)
        {
            string url = "http://localhost:8080/publications";


            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, jsonContent))
            {
                if (!response.IsSuccessStatusCode)
                {

                    new CustomError(response.ReasonPhrase).ShowDialog();
                }
            }
        }

        public static async Task UpdatePublication(string id, StringContent jsonConent)
        {
            string url = "http://localhost:8080/publications/";

            url += id;


            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync(url, jsonConent))
            {
                if (!response.IsSuccessStatusCode)
                {
                    new CustomError(response.ReasonPhrase).ShowDialog();
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
                    new CustomError(response.ReasonPhrase).ShowDialog();
                }

            }
        }
    }
}
