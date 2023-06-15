using Newtonsoft.Json;
using Tera_Web.Entities;

namespace Tera_Web.Models
{
    public class ConsolidatedModel
    {
        public string lblmsj { get; set; }
        List<ConsolidatedObj> ConsolidatedList = new List<ConsolidatedObj>();
        ConsolidatedObj consolidatedObj = new ConsolidatedObj();

        string urlGetlist = "https://localhost:7021/api/Consolidated/GetList";
        string urlGet = "https://localhost:7021/api/Consolidated/GetListFilter";
        public List<ConsolidatedObj> GetConsolidatedList()
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
                    ConsolidatedList = JsonConvert.DeserializeObject<List<ConsolidatedObj>>(resultstr);
                }
                else
                {

                }
            }
            return ConsolidatedList;
        }
        
        public List<ConsolidatedObj> GetConsolidatedListFilter(DateTime min, DateTime max)
        {
            string url = $"{urlGet}?min={min.ToString("yyyy-MM-dd")}&max={max.ToString("yyyy-MM-dd")}";
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.GetAsync(url);
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
                    ConsolidatedList = JsonConvert.DeserializeObject<List<ConsolidatedObj>>(resultstr);
                }
                else
                {

                }
            }
            return ConsolidatedList;

        }



    }
}
