using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Newtonsoft.Json;

namespace CounterTexFront.Services
{
    public static class Microservices
    {
        private static string apiURL = ConfigurationManager.AppSettings["Api"].ToString();

        public static async Task<object> PostWithoutToken(object modelRequest, string endpoint)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiURL);
                    client.DefaultRequestHeaders.Clear();
                    string json = JsonConvert.SerializeObject(modelRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PostAsync(endpoint, content);

                    if (Res.IsSuccessStatusCode)
                    {
                        var res = Res.Content.ReadAsStringAsync().Result;
                        object response = JsonConvert.DeserializeObject<object>(res);
                        return response;
                    }
                    else
                    {
                        throw new Exception("Error: " + Res.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error haciendo la petición al Endpoint: " + endpoint + ", " + ex.ToString());
            }
        }

        public static async Task<object> PostWithToken(object modelRequest, string endpoint)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionHelper.BearerToken))
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiURL);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionHelper.BearerToken);
                        string json = JsonConvert.SerializeObject(modelRequest);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        HttpResponseMessage Res = await client.PostAsync(endpoint, content);

                        if (Res.IsSuccessStatusCode)
                        {
                            var res = Res.Content.ReadAsStringAsync().Result;
                            object response = JsonConvert.DeserializeObject<object>(res);
                            return response;
                        }
                        else
                        {
                            throw new Exception("Error: " + Res.StatusCode);
                        }
                    }
                }
                else
                {
                    throw new Exception("Error: Token no existe o esta expierado");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error haciendo la petición post al Endpoint: " + endpoint);
            }
        }

        public static async Task<object> GetWithoutToken(object modelRequest, string endpoint)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionHelper.BearerToken))
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiURL);
                        client.DefaultRequestHeaders.Clear();
                        string json = JsonConvert.SerializeObject(modelRequest);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        HttpResponseMessage Res = await client.GetAsync(endpoint);

                        if (Res.IsSuccessStatusCode)
                        {
                            var res = Res.Content.ReadAsStringAsync().Result;
                            object response = JsonConvert.DeserializeObject<object>(res);
                            return response;
                        }
                        else
                        {
                            throw new Exception("Error: " + Res.StatusCode);
                        }
                    }
                }
                else
                {
                    throw new Exception("Error: Token no existe o esta expierado");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error haciendo la petición post al Endpoint: " + endpoint);
            }
        }

        public static async Task<object> GetWithToken(object modelRequest, string endpoint)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionHelper.BearerToken))
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiURL);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionHelper.BearerToken);
                        string json = JsonConvert.SerializeObject(modelRequest);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        HttpResponseMessage Res = await client.GetAsync(endpoint);

                        if (Res.IsSuccessStatusCode)
                        {
                            var res = Res.Content.ReadAsStringAsync().Result;
                            object response = JsonConvert.DeserializeObject<object>(res);
                            return response;
                        }
                        else
                        {
                            throw new Exception("Error: " + Res.StatusCode);
                        }
                    }
                }
                else
                {
                    throw new Exception("Error: Token no existe o esta expierado");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error haciendo la petición post al Endpoint: " + endpoint);
            }
        }
    }
}