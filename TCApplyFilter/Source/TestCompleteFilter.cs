namespace TCApplyFilter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class TestCompleteFilter : IIncludeFilter<string>
    {
        public IEnumerable<string> FilterItems(string input, string output, IList<string> includeItems)
            => ProcessItems(input, output, testItem => ApplyFilter(testItem, includeItems));

        /// <inheritdoc />
        public IEnumerable<string> ListItems(string input)
            => ProcessItems(input, null, PrintItem);

        public IEnumerable<string> ListTags(string input)
            => ProcessItems(input, null, ListTags).SelectMany(t => t).Distinct();

        private static IEnumerable<T> ProcessItems<T>(string input, string output, Func<XElement, T> itemProcessor)
        {
            var doc = XDocument.Load(input);
            var testItems = doc.Descendants("testItem");
            foreach (var testItem in testItems.Where(IsNotGroup))
            {
                yield return itemProcessor(testItem);
            }

            if (output != null)
            {
                doc.Save(output);
            }
        }

        private static IEnumerable<string> ListTags(XElement testItem)
            => GetTags(testItem);

        private static string PrintItem(XElement testItem)
        {
            var enabled = bool.Parse(testItem.Attribute("enabled")?.Value ?? "false")
                ? "enabled " : "disabled";
            var description = testItem.Attribute("description")?.Value;
            return $"{enabled} {description}\t{GetTestName(testItem)}";
        }

        private static string ApplyFilter(XElement testItem, IList<string> includeItems)
        {
            var include = IncludeItem(testItem, includeItems);
            testItem.SetAttributeValue("enabled", include ? "True" : "False");
            return $"{(include ? "X " : "  ")}{GetTestName(testItem)}";
        }

        private static string GetTestName(XElement element)
            => $"{element.Attribute("name")?.Value}.{GetMoniker(element)}";

        // TODO: error handling may be required here.
        private static string GetMoniker(XElement element)
            => element.Attribute("testMoniker").Value.Split('}')[1];

        private static bool IsNotGroup(XElement element) => !bool.Parse(element.Attribute("group").Value);

        private static bool IncludeItem(XElement element, IList<string> includeItems)
            => GetTags(element).Intersect(includeItems).Any();

        private static string[] GetTags(XElement element)
            => element.Attribute("description")?.Value
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray()
                ?? new string[0];
    }
}
