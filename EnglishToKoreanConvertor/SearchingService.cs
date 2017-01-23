using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishToKoreanConvertor
{
    class SearchingService : ISearchingService
    {
        #region define 'SINGLETON'
        private static SearchingService INSTANCE = null;

        public static SearchingService getInstance()
        {
            if(INSTANCE == null)
            {
                INSTANCE = new SearchingService();
            }

            return INSTANCE;
        }
        #endregion

        private static readonly string domain = "http://dic.daum.net";
        private static readonly string uri = "/search.do";
        private static readonly string language = "eng";
        
        string ISearchingService.searchMeaning(string keyword)
        {
            HtmlNodeCollection searchResult = this.searchMeaning(keyword);

            if(searchResult == null)
            {
                searchResult = this.searchMeaningByWordId(keyword);
            }

            return (searchResult == null) ? "Not Exist..." : this.formatResult(searchResult);
        }
        private HtmlNodeCollection searchMeaning(string keyword)
        {
            /* [1]. Request HTML. */
            string resultHtml = HttpClientUtil.requestByGet(SearchingService.domain + SearchingService.uri, this.makeQueryArgs(keyword, SearchingService.language));

            HtmlDocument doc = HtmlParsingUtil.load(resultHtml);

            /* [2]. Select '<ul class='list_search'> */
            HtmlNodeCollection nodeList = HtmlParsingUtil.findNode(doc, "ul", "class", "list_search");

            /* [3]. Select '<span class='txt_search'> */
            return (nodeList == null) ? null : HtmlParsingUtil.findNode(nodeList[0], "span", "class", "txt_search");
        }

        private HtmlNodeCollection searchMeaningByWordId(string keyword)
        {
            string wordIdUri = this.searchWordIdUri(keyword);

            if(wordIdUri == null)
            {
                return null;
            }

            return this.searchMeaningByWordId(keyword, wordIdUri);
        }

        private HtmlNodeCollection searchMeaningByWordId(string keyword, string wordUri)
        {
            /* [1]. Request HTML. */
            string resultHtml = HttpClientUtil.requestByGet(SearchingService.domain + wordUri);

            HtmlDocument doc = HtmlParsingUtil.load(resultHtml);

            /* [2]. Select '<ul class='list_mean'> */
            HtmlNodeCollection nodeList = HtmlParsingUtil.findNode(doc, "ul", "class", "list_mean");

            /* [3]. Select '<span class='txt_mean'> */
            return (nodeList == null)? null : HtmlParsingUtil.findNode(nodeList[0], "span", "class", "txt_mean");
        }

        private string searchWordIdUri(string keyword)
        {
            /* [1]. Request HTML. */
            string resultHtml = HttpClientUtil.requestByGet(SearchingService.domain + SearchingService.uri, this.makeQueryArgs(keyword, SearchingService.language));

            HtmlDocument doc = HtmlParsingUtil.load(resultHtml);

            /* [2]. Parse wordIdUri. */
            HtmlNodeCollection nodeList = HtmlParsingUtil.findNode(doc, "meta", "http-equiv", "Refresh");

            /* [3]. Makeup wordIdUri. */
            return (nodeList == null) ? null : this.makeWordUri(nodeList[0]);
        }

        private string makeWordUri(HtmlNode htmlNode)
        {
            string wordUri = htmlNode.Attributes["content"].Value;
            //wordUri = "0; URL=/word/view.do?wordid=ekw000046582&q=Dictionary"
            return (wordUri == null || "".Equals(wordUri)) ? null : wordUri.Substring(wordUri.IndexOf("=") + 1);
        }

        private Dictionary<string, string> makeQueryArgs(string keyword, string language)
        {
            Dictionary<string, string> querys = new Dictionary<string, string>();

            querys["q"] = keyword;
            querys["dic"] = language;

            return querys;
        }
        
        private string formatResult(HtmlNodeCollection searchResult)
        {
            string result = "";

            foreach(HtmlNode node in searchResult)
            {
                result += node.InnerText + "\n";
            }

            return result;
        }

    }
}
