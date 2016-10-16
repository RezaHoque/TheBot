using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBot.Api.Models
{
    public class BotEntity
    {
        [BsonId]
        public string Id { get; set; }
        public string EntityName { get; set; }
        public string Details { get; set; }
        public string Source { get; set; }
        public string ActionDate { get; set; }

        [BsonExtraElements]
        public BsonDocument ExtraElement { get; set; }
    }
}