using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchReposMVCProject.Src.Services.Abstract
{
    public interface ISearchEngine
    {
        string GetResult(string searchStr);
    }
}
