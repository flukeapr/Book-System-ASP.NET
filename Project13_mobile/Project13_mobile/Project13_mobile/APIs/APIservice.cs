using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Project13_mobile.Models;
using Xamarin.Essentials;

namespace Project13_mobile.APIs
{
    class APIservice
    {
        HttpClient client;
        public APIservice()
        {
            client = new HttpClient();

        }
        public async Task<ObservableCollection<Book>> GetBook()
        {
            ObservableCollection<Book> Item = null;

            try
            {
                var response = await client.GetAsync("http://10.0.2.2:49544/api/BooksAPI");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Item = JsonConvert.DeserializeObject<ObservableCollection<Book>>(content);
                    return Item;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return null;
        }
        public async Task<bool> AddBook(Book Item)
        {
            try
            {
                string json = JsonConvert.SerializeObject(Item);

                StringContent sContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://10.0.2.2:49544/api/BooksAPI", sContent);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            
        }
        public async Task<bool> EditBook(int bookId, Book updatedBook)
        {
            try
            {
                string url = "http://10.0.2.2:49544/api/BooksAPI/" + bookId;

                string json = JsonConvert.SerializeObject(updatedBook);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(url, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public async Task<bool> DeleteBook(int bookId)
        {
            try
            {
                string apiUrl = $"http://10.0.2.2:49544/api/BooksAPI/{bookId}";

                var response = await client.DeleteAsync(apiUrl);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public async Task<bool> Register(Register Item)
        {
            try
            {
                string json = JsonConvert.SerializeObject(Item);

                StringContent sContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://10.0.2.2:61160/api/Account/Register", sContent);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }


        }
        public async Task<bool> Login(Login login)
        {
            User Item = null;
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("Content-Type", "application/x-www-form-urlencode");
                dict.Add("grant_type", "password");
                dict.Add("username", login.Email);
                dict.Add("password", login.Password);
                var response = await client.PostAsync("http://10.0.2.2:61160/token", new FormUrlEncodedContent(dict));
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Item = JsonConvert.DeserializeObject<User>(content);
                    Preferences.Set("username", Item.userName);
                    Preferences.Set("token_type", Item.token_type);
                    Preferences.Set("access_token", Item.access_token);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return false;
        }

        public async Task<bool> Logout()
        {
            try
            {
                var access_token = Preferences.Get("access_token", "");

                var token_type = Preferences.Get("token_type", "");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + access_token);
                var response = await client.PostAsync("http://10.0.2.2:61160/api/Account/Logout", null);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }


        }
    }
}
