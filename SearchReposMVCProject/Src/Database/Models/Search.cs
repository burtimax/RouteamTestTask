using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchReposMVCProject.Src.Database.Models
{
    /// <summary>
    /// Database model
    /// </summary>
    public class Search
    {
        public int Id { get; set; }

        /// <summary>
        /// Строка поиска
        /// </summary>
        public string SearchString { get; set; }

        /// <summary>
        /// Json результат
        /// </summary>
        public string ResultData { get; set; }
    }
}
