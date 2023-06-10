using Newtonsoft.Json;
using Tera_Web.Entities;

namespace Tera_Web.Models
{
    public class OrderDetailModel
    {
        string urlGetlist = "https://localhost:7021/api/OrderDetails/GetList";
        string urlGetlistUser = "https://localhost:7021/api/OrderDetails/GetOrderDetailsListUser";
        string urlGet = "https://localhost:7021/api/OrderDetails/GetOrderDetails";

        public string lblmsj { get; set; }
        List<OrderDetailObj> OrderDetailsList = new List<OrderDetailObj>();
        OrderDetailObj orderDetailsObj = new OrderDetailObj();



        //*************************************

        public List<OrderDetailObj> GetOrderDetailsList()
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
                    OrderDetailsList = JsonConvert.DeserializeObject<List<OrderDetailObj>>(resultstr);
                }
                else
                {

                }

            }
            return OrderDetailsList;
        }


        //*************************************

        public List<OrderDetailObj> GetOrderDetailsListUser(int orderId)
        {
            //IEnumerable<Carrera> A;
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.GetAsync(urlGetlistUser + "/" + orderId.ToString());
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
                    OrderDetailsList = JsonConvert.DeserializeObject<List<OrderDetailObj>>(resultstr);
                }
                else
                {

                }

            }
            return OrderDetailsList;
        }

        //*************************************

        //We query the API to fill an object that will be edited.
        public OrderDetailObj GetOrder(int id)
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
                    orderDetailsObj = JsonConvert.DeserializeObject<OrderDetailObj>(resultstr);
                }
                else
                {

                }
            }
            return orderDetailsObj;
        }


        public List<ProductObj>? ComboBoxProduct(/*string token*/)
        {
            using (HttpClient acceso = new HttpClient())
            {
                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/GetTiposUsuario";
                string urlApi = "https://localhost:7021/" + "api/OrderDetails/ComboBoxConsultProducts";

                //acceso.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage respuesta = acceso.GetAsync(urlApi).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<List<ProductObj>>().Result;
                else
                    return null;
            }
        }

        public string Register(OrderDetailObj orderDetailsObj)
        {
            using (HttpClient acceso = new HttpClient())
            {

                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/OrderDetails/Register";

                JsonContent contenido = JsonContent.Create(orderDetailsObj);

                HttpResponseMessage respuesta = acceso.PostAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;

            }
        }

        //**************************************

        public string EditOrderDetails(OrderDetailObj orderDetailsObj)
        {
            using (HttpClient acceso = new HttpClient())
            {


                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/OrderDetails/EditOrderDetails";

                JsonContent contenido = JsonContent.Create(orderDetailsObj);


                HttpResponseMessage respuesta = acceso.PutAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;
            }
        }

        //**************************************

        public string GetOrderDetailsUser()
        {
            using (HttpClient acceso = new HttpClient())
            {


                //string urlApi = "http://localhost/SERVICE/" + "api/UsuarioApi/CreateUsuario";
                string urlApi = "https://localhost:7021/" + "api/OrderDetails/EditOrderDetails";

                JsonContent contenido = JsonContent.Create(orderDetailsObj);


                HttpResponseMessage respuesta = acceso.PutAsync(urlApi, contenido).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return "OK";
                }
                return string.Empty;
            }
        }

        //**************************************

        public void DeleteOrderDetails(int id)
        {
            //validacion de datos para eliminar los datos
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7021/");
                    var response = client.DeleteAsync("api/OrderDetails/DeleteOrderDetails?Rol=" + id);
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
    }
}
