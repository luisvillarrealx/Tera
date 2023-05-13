using Newtonsoft.Json;
using System.Net.Http.Headers;
using Tera_Web.Entities;

namespace Tera_Web.Models
{
    public class ProductModel
    {

        string urlGetList = "https://localhost:7021/api/Product/GetList";
        string urlGet = "https://localhost:7021/api/Product/GetProducto";


        public string lblmsj { get; set; }
        List<ProductObj> ProducList = new List<ProductObj>();
        ProductObj productObj = new ProductObj();

        //llamo para poder mostrar la lista de los productos en la vista
        public List<ProductObj> GetProductList()
        {
            //IEnumerable<Carrera> A;
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    
                    return await client.GetAsync(urlGetList);
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
                    ProducList = JsonConvert.DeserializeObject<List<ProductObj>>(resultstr);
                }
                else
                {

                }
            }
            return ProducList;
        }
        //llamo para llenar la pagina de Edit productos
        public ProductObj GetProduct(int id)
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
                    productObj = JsonConvert.DeserializeObject<ProductObj>(resultstr);
                }
                else
                {

                }
            }
            return productObj;
        }
        #region PostCursos

        public string PostProduct(ProductObj Prod)
        {
            using (HttpClient acceso = new HttpClient())
            {

                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7019/" + "api/Product/Register";

                JsonContent contenido = JsonContent.Create(Prod);

                
                HttpResponseMessage respuesta = acceso.PostAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;

            }
        }
        #endregion
        #region PUTRoles
        public string PutProduct(ProductObj Prod)
        {
            using (HttpClient acceso = new HttpClient())
            {


                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7019/" + "api/Product/EditProduct";

                JsonContent contenido = JsonContent.Create(Prod);

                
                HttpResponseMessage respuesta = acceso.PutAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;
            }
        }
        #endregion

        //**************************************

        public void DeleteProduct(int id)
        {
            //validacion de datos para eliminar los datos
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7021/");
                    var response = client.DeleteAsync("api/Product/DeleteProduct?Rol=" + id);
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
