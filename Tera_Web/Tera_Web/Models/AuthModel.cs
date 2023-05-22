using Tera_Web.Entities;
namespace Tera_Web.Models
{
    public class AuthModel
    {
        string urlPut = "https://localhost:7021/ActualizarRol?IdRol=11&Roles=Admin";
        string urlPuut = "https://localhost:7021/ActualizarRol?IdRol=11&Roles=Admin";


        public string lblmsj { get; set; }

        UserLoginObj UserLoginObj = new UserLoginObj();

        public UserLoginObj? UserValidate(UserLoginObj userLoginObj)
        {
            using (HttpClient acceso = new HttpClient())
            {
                string urlApi = "https://localhost:7021/api/Auth/UserValidate";
                JsonContent contenido = JsonContent.Create(userLoginObj);

                HttpResponseMessage respuesta = acceso.PostAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<UserLoginObj>().Result;
                else
                    return null;
            }
        }
    }
}
