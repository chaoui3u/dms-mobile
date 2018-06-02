﻿using System;
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
                
                var accessTokenExpiration = DateTime.Now.AddSeconds(jwtDynamic.Value<int>("expires_in"));
                var accessToken = jwtDynamic.Value<string>("access_token");

                //saving access token in preferences
                Settings.AccessTokenExpirationDate = accessTokenExpiration;

                System.Diagnostics.Debug.WriteLine(response.StatusCode + " / " + response.ReasonPhrase + " / " + response.RequestMessage);
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

            System.Diagnostics.Debug.WriteLine(response.StatusCode + " / " + response.ReasonPhrase + " / " + response.RequestMessage);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ModifyUserAsync(string accessToken,Guid userId, string firstName, string lastName,
            string password, string email, string role)
        {
            var client = new HttpClient();

            //using the token given
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

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
            HttpResponseMessage response;

            if (Constants.MyUser.Role == "Admin")
             response = await client.PutAsync(Constants.GetUsersUrl+userId, content);
            else
             response = await client.PutAsync(Constants.GetMyUserUrl, content);
            System.Diagnostics.Debug.WriteLine(response.StatusCode + " / " + response.ReasonPhrase+" / "+response.RequestMessage);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<UserModel>> GetUsersAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
            //validating certificate to use https
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidation.MyRemoteCertificateValidationCallback;

            var json = await client.GetStringAsync(Constants.GetUsersUrl);

            JObject jsonResponse = JObject.Parse(json);
            JArray objResponse = (JArray)jsonResponse["value"];
            //IList<UserModel> user = objResponse.ToObject<IList<UserModel>>();

            var users = JsonConvert.DeserializeObject<List<UserModel>>(objResponse.ToString());

            //var users = JsonConvert.DeserializeObject<List<UserModel>>(json);

            return users;

        }
        public async Task<bool> DeleteUserAsync(string accessToken,Guid userId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
            //validating certificate to use https
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidation.MyRemoteCertificateValidationCallback;

            var response = await client.DeleteAsync(Constants.GetUsersUrl + userId);

            System.Diagnostics.Debug.WriteLine(response.StatusCode + " / " + response.ReasonPhrase + " / " + response.RequestMessage);

            return response.IsSuccessStatusCode;
        }

            public async Task<UserModel> GetMyUserAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
            //validating certificate to use https
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidation.MyRemoteCertificateValidationCallback;

            var json = await client.GetStringAsync(Constants.GetMyUserUrl);
            JObject jsonResponse = JObject.Parse(json);

            var user = JsonConvert.DeserializeObject<UserModel>(jsonResponse.ToString());
            return user;
        }



        public async Task<List<WeatherRecordModel>> GetWeatherRecordsAsync(string accessToken,DateTimeOffset dateTime)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
            //validating certificate to use https
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidation.MyRemoteCertificateValidationCallback;


            var month = (int.Parse(dateTime.AddDays(1).Day.ToString()) == 1) ? dateTime.AddMonths(1).Month : dateTime.Month;
            var year = (int.Parse(dateTime.AddMonths(1).Month.ToString()) == 1 && int.Parse(dateTime.AddDays(1).Day.ToString()) == 1) ? dateTime.AddYears(1).Year : dateTime.Year;

            var url = Constants.GetWeatherRecordsUrl + dateTime.Year+"-"+dateTime.Month+"-"+dateTime.Day+"T00:10:00" 
                + "/" + year + "-" + month  + "-" + dateTime.AddDays(1).Day + "T00:00:00";
            var json = await client.GetStringAsync(url);
          
            JObject jsonResponse = JObject.Parse(json);
            JArray objResponse = (JArray)jsonResponse["value"];

            var weatherRecords = JsonConvert.DeserializeObject<List<WeatherRecordModel>>(objResponse.ToString());

            return weatherRecords;
        }

        public async Task<List<WeatherRecordModel>> GetActualWeatherRecord(string accessToken,DateTimeOffset currentDate)
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
            //validating certificate to use https
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidation.MyRemoteCertificateValidationCallback;
            var month = (int.Parse(currentDate.Day.ToString()) == 1 
                && int.Parse(currentDate.Minute.ToString()) >= 0 
                && int.Parse(currentDate.Minute.ToString()) < 20
                && int.Parse(currentDate.Hour.ToString())== 0) ? currentDate.AddMonths(-1).Month : currentDate.Month;
            var year = (int.Parse(currentDate.Month.ToString()) == 1 
                && int.Parse(currentDate.Day.ToString()) == 1
                && int.Parse(currentDate.Minute.ToString()) >= 0
                && int.Parse(currentDate.Minute.ToString()) < 20
                && int.Parse(currentDate.Hour.ToString()) == 0) ? currentDate.AddYears(-1).Year : currentDate.Year;
            var day = (int.Parse(currentDate.Minute.ToString()) >= 0 
                && int.Parse(currentDate.Minute.ToString()) < 20
                && int.Parse(currentDate.Hour.ToString()) == 0) ? currentDate.AddDays(-1).Day : currentDate.Day;
            var hour = (int.Parse(currentDate.Minute.ToString()) >= 0 
                && int.Parse(currentDate.Minute.ToString()) < 20) ? currentDate.AddHours(-1).Hour : currentDate.Hour;

            var url = Constants.GetWeatherRecordsUrl + year + "-" + month + "-" + day + "T"
                + hour.ToString("00.##") + ":" + currentDate.AddMinutes(-20).Minute.ToString("00.##") + ":00" + "/" + currentDate.Year + "-" + currentDate.Month + "-" + currentDate.Day + "T"
                + currentDate.Hour.ToString("00.##") + ":" + currentDate.Minute.ToString("00.##") + ":00";

            var json = await client.GetStringAsync(url);
            JObject jsonResponse = JObject.Parse(json);
            JArray objResponse = (JArray)jsonResponse["value"];

            var weatherRecord = JsonConvert.DeserializeObject<List<WeatherRecordModel>>(objResponse.ToString());
            return weatherRecord;
        }
    }
}
