using HeartlandArtifact.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HeartlandArtifact.Helpers
{
    public class ApiData
    {
        public async Task<ResponseModel<T>> PostData<T>(string extURL, object sendData, bool authHeaders = false)
        {
            ResponseModel<T> data = new ResponseModel<T>();
            string url = string.Format("{0}{1}", ApiUrlConstant.BASE_URL, extURL);
            try
            {
                HttpClient client = new HttpClient();
                // SetAuthHeaders(client, authHeaders);

                HttpContent content;
                if (sendData != null)
                {
                    var json = JsonConvert.SerializeObject(sendData);
                    content = new StringContent(json, Encoding.UTF8, "application/json");
                    //content = new StringContent(json);
                }
                else
                {
                    content = new StringContent(string.Empty);
                }
                // SetAuthHeaders(client, authHeaders);
                try
                {
                    var result = await client.PostAsync(url, content);

                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();
                        data = JsonConvert.DeserializeObject<ResponseModel<T>>(response);
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        data.message = "Unauthorized";
                        data.status = "fail";
                    }
                    else
                    {
                        var response = await result.Content.ReadAsStringAsync();
                        data = JsonConvert.DeserializeObject<ResponseModel<T>>(response);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                data = default(ResponseModel<T>);
            }
            return data;
        }
        public async Task<ResponseModel<T>> GetData<T>(string extURL, bool authHeaders = true)
        {
            ResponseModel<T> data = new ResponseModel<T>();
            string url = string.Format("{0}{1}", ApiUrlConstant.BASE_URL, extURL);
            try
            {
                HttpClient client = new HttpClient();
                // SetAuthHeaders(client, authHeaders);

                var result = await client.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    // var s = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                    //var response = JsonConvert.SerializeObject(result.Content.ReadAsStringAsync(), s);
                    var response = await result.Content.ReadAsStringAsync();
                    try
                    {
                        data = JsonConvert.DeserializeObject<ResponseModel<T>>(response);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    data.message = "Unauthorized";
                    data.status = "fail";
                }
                else
                {
                    var response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ResponseModel<T>>(response);
                }
            }
            catch (Exception ex)
            {
                data = default(ResponseModel<T>);
            }
            return data;
        }
        public async Task<ResponseModel<T>> PutData<T>(string extURL, object sendData, bool authHeaders = false)
        {
            ResponseModel<T> data = new ResponseModel<T>();
            string url = string.Format("{0}{1}", ApiUrlConstant.BASE_URL, extURL);
            try
            {
                HttpClient client = new HttpClient();
                // SetAuthHeaders(client, authHeaders);

                HttpContent content;
                if (sendData != null)
                {
                    var json = JsonConvert.SerializeObject(sendData);
                    content = new StringContent(json, Encoding.UTF8, "application/json");
                    //content = new StringContent(json);
                }
                else
                {
                    content = new StringContent(string.Empty);
                }
                // SetAuthHeaders(client, authHeaders);
                try
                {
                    var result = await client.PutAsync(url, content);

                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();
                        data = JsonConvert.DeserializeObject<ResponseModel<T>>(response);
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        data.message = "Unauthorized";
                        data.status = "fail";
                    }
                    else
                    {
                        var response = await result.Content.ReadAsStringAsync();
                        data = JsonConvert.DeserializeObject<ResponseModel<T>>(response);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                data = default(ResponseModel<T>);
            }
            return data;
        }
        public async Task<ResponseModel<T>> DeleteData<T>(string extURL, bool authHeaders = true)
        {
            ResponseModel<T> data = new ResponseModel<T>();
            string url = string.Format("{0}{1}", ApiUrlConstant.BASE_URL, extURL);
            try
            {
                HttpClient client = new HttpClient();
                // SetAuthHeaders(client, authHeaders);

                var result = await client.DeleteAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    // var s = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                    //var response = JsonConvert.SerializeObject(result.Content.ReadAsStringAsync(), s);
                    var response = await result.Content.ReadAsStringAsync();
                    try
                    {
                        data = JsonConvert.DeserializeObject<ResponseModel<T>>(response);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    data.message = "Unauthorized";
                    data.status = "fail";
                }
                else
                {
                    var response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ResponseModel<T>>(response);
                }
            }
            catch (Exception ex)
            {
                data = default(ResponseModel<T>);
            }
            return data;
        }
    }
}
