using System;
using System;
using System.IO;
using HtmlAgilityPack;

namespace AutoTest
{
    class MetronodeLTCopperLinkTester
    {
        public const int PORT_COUNT = 12;
        
        public static bool[] GetStatuses(String html)
        {
            bool[] falsy = new bool[PORT_COUNT];
            try
            {
                bool[] linked = new bool[PORT_COUNT];
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);
                HtmlNodeCollection nodeCollection = document.DocumentNode.SelectNodes("//td/img");
                int portIdx = 0;
                foreach (HtmlNode nodeElem in nodeCollection)
                {
                    // If the port index represents a non SFP port then we have iterated over all SFP ports
                    if (portIdx >= PORT_COUNT) { break; }
                    string bubbleImage = (nodeElem.GetAttributeValue("src", "null"));
                    if (bubbleImage.Equals("green.gif"))
                    {
                        linked[portIdx] = true;
                    }
                    else
                    {
                        linked[portIdx] = false;
                    }
                    portIdx += 1;
                }
                return linked;

            }
            catch
            {
                return falsy;
            }
            
        }
    }
}
