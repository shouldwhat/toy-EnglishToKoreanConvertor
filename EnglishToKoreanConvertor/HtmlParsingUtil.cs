using HtmlAgilityPack;
using System.Collections.Generic;

namespace EnglishToKoreanConvertor
{
    public static class HtmlParsingUtil
    {
        public static HtmlDocument load(string html)
        {
            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(html);

            return doc;
        }

        public static HtmlNodeCollection findNode(HtmlDocument doc, string nodeName, string attrName, string attrValue)
        {
            return doc.DocumentNode.SelectNodes(string.Format("//{0}[@{1}='{2}']", nodeName, attrName, attrValue));
        }

        public static HtmlNodeCollection findNode(HtmlNode node, string nodeName, string attrName, string attrValue)
        {
            return node.SelectNodes(string.Format(".//{0}[@{1}='{2}']", nodeName, attrName, attrValue));
        }
    }
}
