using Newtonsoft.Json;
using Tera_Web.Entities;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;

namespace Tera_Web.Models
{
    public class UserModel
    {
        public string lblmsj { get; set; }
        List<UserObj> credentialList = new List<UserObj>();
        UserObj credentialObj = new UserObj();

        string urlGetlist = "https://localhost:7021/api/User/GetList";
        string urlGet = "https://localhost:7021/api/User/GetList";

        public List<UserObj> GetUser()
        {
            //IEnumerable<Carrera> A;
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.GetAsync(urlGet);
                }
                );
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultstr = task2.Result;
                    credentialList = JsonConvert.DeserializeObject<List<UserObj>>(resultstr);
                }
                else
                {

                }
            }
            return credentialList;
        }

        //We query the API to fill an object that will be edited.
        public UserObj GetUser(int id)
        {
            //IEnumerable<Carrera> A;
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.GetAsync(urlGet + "/" + id.ToString());
                }
                );
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultstr = task2.Result;
                    credentialObj = JsonConvert.DeserializeObject<UserObj>(resultstr);
                }
                else
                {

                }
            }
            return credentialObj;
        }

        public string PostUsers(UserObj userObj)
        {
            using (HttpClient access = new HttpClient())
            {
                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/User/Register";

                JsonContent content = JsonContent.Create(userObj);

                HttpResponseMessage response = access.PostAsync(urlApi, content).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;
            }
        }

        public string PutUsers(UserObj userObj)
        {
            using (HttpClient access = new HttpClient())
            {
                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/User/EditUser";

                JsonContent content = JsonContent.Create(userObj);

                HttpResponseMessage response = access.PutAsync(urlApi, content).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;
            }
        }
    }
}
