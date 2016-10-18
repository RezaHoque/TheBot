using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TheBot.Data.Services;
using TheBot.Data.DataAccess;
using Microsoft.AspNet.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheBot.Web.Controllers
{
    public class EntityController : Controller
    {
        private readonly IEntityService entityService;
        public EntityController(IEntityService service)
        {
            entityService = service;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Add(IFormCollection frm)
        {
            
            var entityName = frm["entityname"];
            var details = frm["details"];
            var source = frm["source"];
            var tags = frm["tags"];
            var entity = new TheBot.Data.Entities.BotEntity();
            entity.entityname = entityName;
            entity.details = details;
            entity.source = source;
            entity.tags = tags.ToString().Split(',');
            entity.actiondate = DateTime.Now.ToShortDateString();



            entityService.AddEntity(entity);

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var documents = await entityService.GetEntities();

            return View(documents);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await entityService.DeleteEntity(id);

            return Content(Boolean.TrueString);
        }
    }
}
