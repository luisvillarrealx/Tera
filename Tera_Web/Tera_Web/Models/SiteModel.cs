using Newtonsoft.Json;
using Tera_Web.Entities;

namespace Tera_Web.Models
{
    public class SiteModel
    {
        string urlGetlist = "https://localhost:7021/api/Site/GetList";
        string urlGet = "https://localhost:7021/api/Site/GetSite";

        public string lblmsj { get; set; }
        List<SiteObj> SiteList = new List<SiteObj>();
        SiteObj siteObj = new SiteObj();



        //*************************************

        public List<SiteObj> GetSiteList()
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
                    SiteList = JsonConvert.DeserializeObject<List<SiteObj>>(resultstr);
                }
                else
                {

                }

            }
            return SiteList;
        }
        //We query the API to fill an object that will be edited.
        public SiteObj GetSite(int id)
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
                    siteObj = JsonConvert.DeserializeObject<SiteObj>(resultstr);
                }
                else
                {

                }
            }
            return siteObj;
        }

        public string Register(SiteObj SiteObj)
        {
            using (HttpClient acceso = new HttpClient())
            {

                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/Site/Register";

                JsonContent contenido = JsonContent.Create(SiteObj);

                HttpResponseMessage respuesta = acceso.PostAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;

            }
        }

        //**************************************

        public string EditSide(SiteObj siteObj)
        {
            using (HttpClient acceso = new HttpClient())
            {


                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/Site/EditSite";

                JsonContent contenido = JsonContent.Create(siteObj);


                HttpResponseMessage respuesta = acceso.PutAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;
            }
        }

        //**************************************

        public void DeleteSite(int id)
        {
            //validacion de datos para eliminar los datos
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7021/");
                    var response = client.DeleteAsync("api/Site/DeleteSite?siteId=" + id);
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
                lblmsj = "Error en el registro de la carrera" + ex.StackTrace;
            }
        }
    }
}
