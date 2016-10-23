using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBot.Models;

namespace TheBot.Services
{
    public interface IBotManager
    {
        Task<Luis> GetModelFromLuis(string query);
        Task<string> ProcessResponse(Luis model);
    }
}
