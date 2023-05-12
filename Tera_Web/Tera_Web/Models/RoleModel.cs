using Newtonsoft.Json;
using System.Net.Http.Headers;
using Tera_Web.Entities;

namespace Tera_Web.Models
{
    public class RoleModel
    {

        string urlGetlist = "https://localhost:7021/api/Role/GetList";
        string urlGet = "https://localhost:7021/api/Role/GetRole";

        public string lblmsj { get; set; }
        List<RoleObj> RoleList = new List<RoleObj>();
        RoleObj roleObj = new RoleObj();



        //*************************************

        public List<RoleObj> GetRoleList()
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
                    RoleList = JsonConvert.DeserializeObject<List<RoleObj>>(resultstr);
                }
                else
                {

                }

            }
            return RoleList;
        }
        //We query the API to fill an object that will be edited.
        public RoleObj GetRole(int id)
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
                    roleObj = JsonConvert.DeserializeObject<RoleObj>(resultstr);
                }
                else
                {

                }
            }
            return roleObj;
        }

        public string PostRoles(RoleObj roleObj)
        {
            using (HttpClient acceso = new HttpClient())
            {

                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/Role/Register";

                JsonContent contenido = JsonContent.Create(roleObj);

                HttpResponseMessage respuesta = acceso.PostAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;

            }
        }

        //**************************************

        public string PUTRoles(RoleObj roleObj)
        {
            using (HttpClient acceso = new HttpClient())
            {


                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/Role/EditRole";

                JsonContent contenido = JsonContent.Create(roleObj);


                HttpResponseMessage respuesta = acceso.PutAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;
            }
        }

        //**************************************

        public void DeleteRoles(int id)
        {
            //validacion de datos para eliminar los datos
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7021/");
                    var response = client.DeleteAsync("api/Role/DeleteRole?Rol=" + id);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        lblmsj = "Se ha eliminado exitosamente.";
                    }
                    else
                    {
                        lblmsj = "No se encontro la identificacion para eliminar el Rol.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsj = "Error en el registro de la carrera\n" + ex.StackTrace;
            }
        }

        //**

    }
}
