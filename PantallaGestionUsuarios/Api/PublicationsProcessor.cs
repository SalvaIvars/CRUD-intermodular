using Newtonsoft.Json;
using PantallaGestionUsuarios.Models;
using PantallaGestionUsuarios.Models.Response;
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
        public static async Task<PublicationModel> LoadPublication(string id = "") 
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

                    for(int i = 0; i < publication.data.Length; i++)
                    {
                        photos[i] = publication.data[i];
        
                    }
                    return photos;
                }
                else
                {
                   MessageBox.Show(response.ReasonPhrase);
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
                    throw new Exception(response.ReasonPhrase);
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
