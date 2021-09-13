using SearchReposMVCProject.Src.Database.Contexts;
using SearchReposMVCProject.Src.Database.Models;
using SearchReposMVCProject.Src.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchReposMVCProject.Src.Services.Implementation
{
    public class SearchEngine : ISearchEngine
    {

        SearchContext _db;
        public SearchEngine(SearchContext db)
        {
            this._db = db;
        }

        public string GetResult(string searchStr)
        {
            return SearchRepositoryRequest(searchStr);
        }

        private string SearchRepositoryRequest(string searchStr)
        {
            if(searchStr == null)
            {
                return null;
            }

            searchStr = searchStr.ToLower();
            //Емотрим в локальном хранилище
            if (SearchInDb(searchStr, out var res))
            {
                return res;
            }

            return SearchInGithub(searchStr);
        }

        /// <summary>
        /// Search data in local storage
        /// </summary>
        private bool SearchInDb(string searchStr, out string res)
        {
            Search searchRes = _db.Search.Where(i =>  String.Compare(i.SearchString, searchStr) == 0).FirstOrDefault();
            
            //Получили данные из БД
            if(Equals(searchRes, null) == false)
            {
                res =  searchRes.ResultData;
                return true;
            }

            res = null;
            return false;
        }

        /// <summary>
        /// Post request to GithubAPI
        /// </summary>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        private string SearchInGithub(string searchStr)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");//Set the User Agent to "request"
            string response = client.GetStringAsync($"https://api.github.com/search/repositories?q={searchStr}").Result;
            
            //Сохраняем результат поиска в БД
            Search newSearch = new Search()
            {
                SearchString = searchStr,
                ResultData = response,
            };

            _db.Search.Add(newSearch);
            _db.SaveChanges();

            return response;
        }
    }
}
