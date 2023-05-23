﻿using Tera_Web.Entities;
namespace Tera_Web.Models
{
    public class AuthModel
    {
        public string lblmsj { get; set; }

        UserLoginObj UserLoginObj = new UserLoginObj();

        public UserLoginObj? UserValidate(UserLoginObj userLoginObj)
        {
            using (HttpClient access = new HttpClient())
            {
                string urlApi = "https://localhost:7021/api/Auth/UserValidate";
                JsonContent content = JsonContent.Create(userLoginObj);

                HttpResponseMessage response = access.PostAsync(urlApi, content).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<UserLoginObj>().Result;
                else
                    return null;
            }
        }
    }
}
