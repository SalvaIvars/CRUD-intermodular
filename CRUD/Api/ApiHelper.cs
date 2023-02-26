using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;

namespace CRUD
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }
        public static UserModel ActiveUser { get; set; }
        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void AddToken()
        {
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["accessToken"].ToString());
        }
    }
}
