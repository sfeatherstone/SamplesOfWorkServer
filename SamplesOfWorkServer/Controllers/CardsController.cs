using System;
using System.Collections.Generic;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace SamplesOfWorkServer.Controllers
{
    [JsonObject(MemberSerialization.OptIn)]
    public class card
    {
        [JsonProperty]
        public String id;
        [JsonProperty]
        public String title;
        [JsonProperty]
        public String image_url;
        public String internalImage_url;
        [JsonProperty]
        public String description;
    }

    public class Cards
    {
        public List<card> cards;
    }

    public class CardsController : ApiController
    {
        static private Cards cards = null;

        static private void buildChats(UrlHelper Url)
        {
            if (cards == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "SamplesOfWorkServer.Controllers.cards.json";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    cards = JsonConvert.DeserializeObject<Cards>(result);
                    foreach (var c in cards.cards)
                    {
                        if (c.internalImage_url != null)
                        {
                            c.image_url = Url.Content(c.internalImage_url);
                        }
                    }
                }
                
            }
        }
        
        // GET api/cards
        [SwaggerOperation("GetAll")]
        public Cards Get()
        {
            buildChats(Url);
            return CardsController.cards;
        }
    }
}
