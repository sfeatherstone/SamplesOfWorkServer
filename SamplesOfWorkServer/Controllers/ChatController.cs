using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;

namespace SamplesOfWorkServer.Controllers
{
    public class chat
    {
        public String username;
        public String content;
        public String userImage_url;
        public String time;
    }

    public class Conversation
    {
        public List<chat> chats;
    }

    public class ChatController : ApiController
    {
        static private Conversation chats = null;

        static private void buildChats()
        {
            if (chats == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "SamplesOfWorkServer.Controllers.chat.json";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    chats = JsonConvert.DeserializeObject<Conversation>(result);
                }
                
            }
        }
        
        // GET api/values
        [SwaggerOperation("GetAll")]
        public Conversation Get()
        {
            buildChats();
            return chats;// new string[] { "value1", "value2" };
        }
    }
}
