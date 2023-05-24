using Newtonsoft.Json;
using Tera_Web.Entities;

namespace Tera_Web.Models
{
    public class OrderModel
    {

        string urlGetListForOrder = "https://localhost:7021/api/Product/GetList";
        string urlGetlist = "https://localhost:7021/api/Order/GetList";
        string urlGet = "https://localhost:7021/api/Order/GetOrder";

        public string lblmsj { get; set; }
        List<OrderObj> OrderList = new List<OrderObj>();
        OrderObj orderObj = new OrderObj();



        //*************************************

        public List<OrderObj> GetOrderList()
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
                    OrderList = JsonConvert.DeserializeObject<List<OrderObj>>(resultstr);
                }
                else
                {

                }

            }
            return OrderList;
        }
        //We query the API to fill an object that will be edited.
        public OrderObj GetOrder(int id)
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
                    orderObj = JsonConvert.DeserializeObject<OrderObj>(resultstr);
                }
                else
                {

                }
            }
            return orderObj;
        }

        public string Register(OrderObj OrderObj)
        {
            using (HttpClient acceso = new HttpClient())
            {

                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/Order/Register";

                JsonContent contenido = JsonContent.Create(OrderObj);

                HttpResponseMessage respuesta = acceso.PostAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;

            }
        }

        //**************************************

        public string EditOrder(OrderObj OrderObj)
        {
            using (HttpClient acceso = new HttpClient())
            {


                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/Order/EditOrder";

                JsonContent contenido = JsonContent.Create(OrderObj);


                HttpResponseMessage respuesta = acceso.PutAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;
            }
        }

        //**************************************

        public void DeleteOrder(int id)
        {
            //validacion de datos para eliminar los datos
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7021/");
                    var response = client.DeleteAsync("api/Order/DeleteOrder?Rol=" + id);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        lblmsj = "Se ha eliminado exitosamente.";
                    }
                    else
                    {
                        lblmsj = "No se encontro la identificacion para eliminar la orden.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsj = "Error en el registro de la carrera" + ex.StackTrace;
            }
        }
        //llamo para poder mostrar la lista de los productos en la vista
        public List<OrderObj> GetProductList()
        {
            //IEnumerable<Carrera> A;
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {

                    return await client.GetAsync(urlGetListForOrder);
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
                    OrderList = JsonConvert.DeserializeObject<List<OrderObj>>(resultstr);
                }
                else
                {

                }
            }
            return OrderList;
        }
    }
}
