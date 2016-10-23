using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TheBot.Models;
using TheBot.Services;
using TheBot.Data.Services;
using TheBot.Data.DataAccess;


namespace TheBot.BusinessObjects
{
    
    public class BotManager : IBotManager
    {
        private readonly IEntityService entityManager;
        public BotManager()
        {
            entityManager = new EntityManager();
        }
        string appId = ConfigurationManager.AppSettings["AppId"];
        string azureSubscriptionKey = ConfigurationManager.AppSettings["AzureSubscriptionKey"];
        string luisEndPoint = ConfigurationManager.AppSettings["LuisEndPoint"];
        /// <summary>
        /// Takes the user input and sends it to LUIS and deserialize the LUIS response to a model.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<Luis> GetModelFromLuis(string query)
        {
            
            query = Uri.EscapeDataString(query);
            Luis model = new Luis();
            using (HttpClient client = new HttpClient())
            {

                string RequestURI = luisEndPoint + appId + "&subscription-key=" + azureSubscriptionKey + "&q=" + query+ "&timezoneOffset=0.0";
                HttpResponseMessage msg = await client.GetAsync(RequestURI);

                if (msg.IsSuccessStatusCode)
                {
                    var JsonDataResponse = await msg.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<Luis>(JsonDataResponse);
                }
            }
            return model;
        }

        /// <summary>
        /// Process the response to present to the user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> ProcessResponse(Luis model)
        {
            string response = "";
            if (model.dialog != null)
            {
                if (model.dialog.status == "Question")
                {
                    response= model.dialog.prompt;

                }
            }
            else if(model.entities.Any())
            {
                var entity = model.entities.FirstOrDefault();
                var resultFromDb = await entityManager.GetEntity(entity.entity);
                if (resultFromDb != null)
                {
                    response = resultFromDb.details;
                }
                else
                {
                    var url = "http://www.bing.com/search?q=" + model.query;
                    response = "My system don't have an answer for that but please try here: " + url;
                }
                
            }
            return response;
        }
    }
}