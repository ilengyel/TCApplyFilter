namespace TCApplyFilter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class TestCompleteFilter : IIncludeFilter<string>
    {
        public void FilterItems(string input, string output, IList<string> includeItems)
        {
            // Note if the xml to Linq performance is too bad then need to change to xml reader/writer
            // or do something like: https://lennilobel.wordpress.com/2009/09/02/streaming-into-linq-to-xml-using-c-custom-iterators-and-xmlreader/
            // ... but meh... we all have GBs of ram..
            var doc = XDocument.Load(input);
            var root = doc.Element("Root");
            var testItems = doc.Descendants("testItem");
            foreach (var testItem in testItems.Where(IsNotGroup))
            {
                var value = IncludeItem(testItem, includeItems) ? "True" : "False";
                testItem.SetAttributeValue("enabled", value);
            }

            doc.Save(output);
        }

        private static bool IsNotGroup(XElement element) => !bool.Parse(element.Attribute("group").Value);

        private static bool IncludeItem(XElement element, IList<string> includeItems)
            => GetTags(element).Intersect(includeItems).Any();

        private static string[] GetTags(XElement element)
            => element.Attribute("description")?.Value
                .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                ?? new string[0];
    }
}
