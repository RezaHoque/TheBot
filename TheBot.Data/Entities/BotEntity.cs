using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBot.Data.Entities
{
    public class BotEntity
    {
        [BsonId]
        public ObjectId id { get; set; }
        public string entityname { get; set; }
        public string details { get; set; }
        public string source { get; set; }
        public string actiondate { get; set; }
        public string[] tags { get; set; }

        //[BsonExtraElements]
        //public BsonDocument ExtraElement { get; set; }
    }
}
