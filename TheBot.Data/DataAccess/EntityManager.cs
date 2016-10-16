using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBot.Data.Entities;
using TheBot.Data.Services;

namespace TheBot.Data.DataAccess
{
    public class EntityManager : IEntityService
    {
        public async Task AddEntity(BotEntity entity)
        {
            var db = GetDatabase();
            var collection = db.GetCollection<BotEntity>("entities");

            //var entity = new BotEntity
            //{
            //    entityname = "victory",
            //    details = "16th December is the victory day of Bangladesh.",
            //    actiondate = DateTime.Now.ToShortDateString(),
            //    tags = new string[] { "victory day","victory","bijoy dibosh"}
            //};

            await collection.InsertOneAsync(entity);
        }

        public async Task<IEnumerable<BotEntity>> GetEntities()
        {
            var db = GetDatabase();
            var collection = db.GetCollection<BotEntity>("entities");

            var documents = await collection.Find(Builders<BotEntity>.Filter.Empty).ToListAsync();
            return documents;
        }

        public async Task<BotEntity> GetEntity(string tag)
        {
            var db = GetDatabase();
            var collection = db.GetCollection<BotEntity>("entities");

            var filter = Builders<BotEntity>.Filter.AnyEq(x=>x.tags,tag);
            var document = await collection.Find(filter).FirstOrDefaultAsync();

            return document;
        }
        private IMongoDatabase GetDatabase()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("TheBot");

            return db;

        }
        
    }
}
