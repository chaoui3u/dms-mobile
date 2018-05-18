using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MeteoMobile.Helpers;
using MeteoMobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MeteoMobile.Services
{
    public class ApiServices
    {
        public async Task<string> LoginAsync(string userName, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post, Constants.BaseApiAddress + "token");

            request.Content = new FormUrlEncodedContent(keyValues);

            //validating certificate to use https
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidation.MyRemoteCertificateValidationCallback;

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);

                var content = await response.Content.ReadAsStringAsync();

                JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(content);

                var accessTokenExpiration = jwtDynamic.Value<DateTime>(".expires");
                var accessToken = jwtDynamic.Value<string>("access_token");

                //saving access token in preferences
                Settings.AccessTokenExpirationDate = accessTokenExpiration;
               
                System.Diagnostics.Debug.WriteLine(accessTokenExpiration);

                System.Diagnostics.Debug.WriteLine(content);

                return accessToken;
            }

        }

        public async Task<bool> SignUpAsync(string accessToken,string firstName, string lastName,
            string password, string email, string role)
        {
            var client = new HttpClient();

            //using the token given
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer",accessToken);

            var model = new SignUpModel
            {
                FirstName = firstName,
                LastName = lastName,
                Password = password,       
                Email = email,
                Role = role
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            //validating certificate to use https
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidation.MyRemoteCertificateValidationCallback;

            var response = await client.PostAsync(Constants.SignUpUrl, content);

            return response.IsSuccessStatusCode;
        }
    }
}
