using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CelilCavus.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CelilCavus.Manager
{
    public class BaseClientManager<TEntity> : IClientBaseManager<TEntity> where TEntity : class
    {
        private static string _baseUrl;
        static HttpClient _client;
        static string _url;
        private string FullUrl = string.Concat(_baseUrl, _url);

        public void Client()
        {
            _client.BaseAddress = new Uri(_baseUrl.ToString());
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
        }
        public async Task Add(TEntity entity)
        {
            try
            {
                using (_client = new HttpClient())
                {
                    Client();
                    string jsonString = JsonSerializer.Serialize(entity);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync($"{FullUrl}", content);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task DeleteAsync(int id)
        {
            try
            {
                using (_client = new HttpClient())
                {
                    Client();
                    var response = await _client.DeleteAsync($"{FullUrl}/{id}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                using (_client = new HttpClient())
                {
                    Client();
                    string jsonString = JsonSerializer.Serialize(entity);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    var response = await _client.PutAsync($"{FullUrl}", content);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public ICollection<TEntity> GetAll()
        {
            try
            {
                using (_client = new HttpClient())
                {
                    Client();
                    var response = _client.GetAsync($"{_url}").Result;
                    response.EnsureSuccessStatusCode();
                    return response.Content.ReadFromJsonAsync<ICollection<TEntity>>().Result;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<ICollection<TEntity>> GetAllAsycn()
        {
            try
            {
                using (_client = new HttpClient())
                {
                    Client();
                    var response = _client.GetAsync($"{_url}").Result;
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadFromJsonAsync<ICollection<TEntity>>();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public TEntity GetById(int id)
        {
            try
            {
                using (_client = new HttpClient())
                {
                    Client();
                    var response = _client.GetAsync($"{_url}/{id}").Result;
                    var code = response.EnsureSuccessStatusCode();
                    if (code.IsSuccessStatusCode == true)
                    {
                        return response.Content.ReadFromJsonAsync<TEntity>().Result;
                    }
                    else return null;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public Task<TEntity> GetByIdAsycn(int id)
        {
            var NullOrEmptyId = id > 0 ? true : false;
            if (NullOrEmptyId == true)
            {
                try
                {
                    using (_client = new HttpClient())
                    {
                        var response = _client.GetAsync("{_url}/{id}").Result;
                        var code = response.EnsureSuccessStatusCode();
                        if (code.IsSuccessStatusCode == true)
                        {
                            return response.Content.ReadFromJsonAsync<TEntity>();
                        }
                        else return null;
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            else return null;
        }

        public void Url(string baseUrl, string url)
        {
            _url = url;
            _baseUrl = baseUrl;
        }

    }
}
