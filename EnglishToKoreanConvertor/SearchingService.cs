using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishToKoreanConvertor
{
    abstract class SearchingService : ISearchingService
    {
        public abstract string searchMeaning(string keyword);
    }
}
