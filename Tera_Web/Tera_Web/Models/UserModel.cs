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


        public string PostUsers(UserObj userObj)
        {
            using (HttpClient access = new HttpClient())
            {
                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/User/Register";

                JsonContent content = JsonContent.Create(userObj);

                HttpResponseMessage respuesta = access.PostAsync(urlApi, content).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;
            }
        }
    }
}
