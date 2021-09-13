using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchReposMVCProject.Models.Json;
using SearchReposMVCProject.Src.Database.Contexts;
using SearchReposMVCProject.Src.Database.Models;
using SearchReposMVCProject.Src.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SearchReposMVCProject.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : Controller
    {
        
        ISearchEngine _se;
        SearchContext _db;

        public ApiController(ISearchEngine se, SearchContext db)
        {
            _db = db;
            _se = se;
        }

        // POST/GET: ApiController/Find/{search string}
        [Route("find")]
        [HttpPost]
        public IActionResult FindPost()
        {
            if (Request.Form.ContainsKey("search") == false) return Json(null);

            string search = Request.Form["search"];
            return Json(_se.GetResult(search));
        }

        [Route("find")]
        [HttpGet]
        public IActionResult FindGet()
        {
            //ToDo сделать так, чтобы не было свойства [owner/login], а просто [login]

            if (Request.Form.ContainsKey("search") == false) return Json(null);

            string search = Request.Form["search"];

            RepositoryData data = JsonSerializer.Deserialize<RepositoryData>(_se.GetResult(search));
            return Json(data);
        }


        [Route("find/{id}")]
        [HttpDelete]
        public IActionResult FindDelete(int id)
        {
            Search search = _db.Search.Where(s=>s.Id == id).FirstOrDefault();
            if(search != null)
            {
                _db.Search.Remove(search);
                _db.SaveChanges();

                //Возвращаем 204
                return new NoContentResult();
            }

            //ToDo Что здесь возвращать?
            return new NotFoundResult();
        }
    }
}
