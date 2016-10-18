using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBot.Data.Entities;

namespace TheBot.Data.Services
{
    public interface IEntityService
    {
        Task AddEntity(BotEntity entity);
        Task<BotEntity> GetEntity(string tag);
        Task<IEnumerable<BotEntity>> GetEntities();
        Task DeleteEntity(string id);
    }
}
