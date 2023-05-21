using Newtonsoft.Json;
using Tera_Web.Entities;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;

namespace Tera_Web.Models
{
    public class UserModel
    {
        public string lblmsj { get; set; }
        List<UserObj> userList = new List<UserObj>();
        UserObj userObj = new UserObj();

        string urlGetlist = "https://localhost:7021/api/User/GetList";
        string urlGet = "https://localhost:7021/api/User/GetUser";

        public List<UserObj> GetUserList()
        {
            //IEnumerable<Carrera> A;
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.GetAsync(urlGetlist);
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
                    userList = JsonConvert.DeserializeObject<List<UserObj>>(resultstr);
                }
                else
                {

                }
            }
            return userList;
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
                    userObj = JsonConvert.DeserializeObject<UserObj>(resultstr);
                }
                
            }
            return userObj;
        }

        public string PostUsers(UserEPObj userEPObj)
        {
            using (HttpClient access = new HttpClient())
            {
                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/User/Register";

                JsonContent content = JsonContent.Create(userEPObj);

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

        public void DeleteUsers(int id)
        {
            //validacion de datos para eliminar los datos
            try
            {
                using (var client = new HttpClient())
                {
                    
                    client.BaseAddress = new Uri("https://localhost:7021/");
                    var response = client.DeleteAsync("api/User/EliminarUsuario?userId=" + id);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        lblmsj = "Se ha eliminado exitosamente.";
                    }
                    else
                    {
                        lblmsj = "No se encontro la identificacion para eliminar el curso.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsj = "Error en el registro de la carrera\n" + ex.StackTrace;
            }
        }

        public string EmailExists(string validateEmailExists)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:7021//api/EmailExists?validateEmailExists=" + validateEmailExists;
                HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<string>().Result;

                return "ERROR";
            }
        }

        public void ChangeUserActive(long id)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:7021/api/ChangeUserActive?q=" + id;

                HttpResponseMessage response = client.DeleteAsync(url).GetAwaiter().GetResult();
            }
        }

        //En la funcion CheckRoles vamos a crear un combobox para registrar usaurio con Rol
        public List<RoleObj>? ComboBoxRoles(/*string token*/)
        {
            using (HttpClient acceso = new HttpClient())
            {
                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/GetTiposUsuario";
                string urlApi = "https://localhost:7021/" + "api/User/ComboBoxConsultRoles";

                //acceso.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage respuesta = acceso.GetAsync(urlApi).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<List<RoleObj>>().Result;
                else
                    return null;
            }
        }


        //En la funcion CheckRoles vamos a crear un combobox para registrar usaurio con Rol
        public List<SiteObj>? ComboBoxSites(/*string token*/)
        {
            using (HttpClient acceso = new HttpClient())
            {
                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/GetTiposUsuario";
                string urlApi = "https://localhost:7021/" + "api/User/ComboBoxConsultSites";

                //acceso.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage respuesta = acceso.GetAsync(urlApi).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<List<SiteObj>>().Result;
                else
                    return null;
            }
        }
    }
}
