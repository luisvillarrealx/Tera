using Newtonsoft.Json;
using Tera_Web.Entities;

namespace Tera_Web.Models
{
    public class CategoryModel
    {
        string urlGetlist = "https://localhost:7021/api/Category/GetList";
        string urlGet = "https://localhost:7021/api/Category/GetRole";

        public string lblmsj { get; set; }
        List<CategoryObj> RoleList = new List<CategoryObj>();
        CategoryObj CategoryObj = new CategoryObj();

        public List<CategoryObj> GetCategoryList()
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
                    RoleList = JsonConvert.DeserializeObject<List<CategoryObj>>(resultstr);
                }
                else
                {

                }
            }
            return RoleList;
        }
        //We query the API to fill an object that will be edited.
        public CategoryObj GetCategory(int id)
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
                    CategoryObj = JsonConvert.DeserializeObject<CategoryObj>(resultstr);
                }
                else
                {

                }
            }
            return CategoryObj;
        }

        public string PostCategory(CategoryObj CategoryObj)
        {
            using (HttpClient acceso = new HttpClient())
            {

                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/Category/Register";

                JsonContent contenido = JsonContent.Create(CategoryObj);

                HttpResponseMessage respuesta = acceso.PostAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;

            }
        }

        public string PUTCategory(CategoryObj CategoryObj)
        {
            using (HttpClient acceso = new HttpClient())
            {
                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/Category/EditRole";

                JsonContent contenido = JsonContent.Create(CategoryObj);

                HttpResponseMessage respuesta = acceso.PutAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;
            }
        }

        public void DeleteCategory(int categoryId)
        {
            //validacion de datos para eliminar los datos
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7021/");
                    var response = client.DeleteAsync("api/Category/DeleteCategory?categoryId=" + categoryId);
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
        public bool CategoryExist(string CategoryName)
        {
            using (var client = new HttpClient())
            {
                string url = $"https://localhost:7021/api/Category/CategoryExist?CategoryName={CategoryName}";

                try
                {
                    HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        bool emailExists = bool.Parse(responseContent);
                        return emailExists;
                    }
                    else
                    {
                        // Manejar la respuesta no exitosa si es necesario
                        Console.WriteLine("Error en la solicitud: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones si ocurren
                    Console.WriteLine("Error al realizar la solicitud: " + ex.Message);
                }
            }

            return false;
        }
    }
}
