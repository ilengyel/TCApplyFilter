namespace TCApplyFilter.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using TCApplyFilter;
    using Xunit;

    public class TestCompleteFilterTests
    {
        [Fact]
        public void TestInitial()
        {
            // Arrange
            var tags = new List<string> { "one", "two", "three" };
            var target = new TestCompleteFilter();
            var input = Path.Combine("Resources", "Sample1.xml");
            var output = Path.Combine("Sample1_filtered.xml");
            if (File.Exists(output))
            {
                File.Delete(output);
            }

            // Act
            target.FilterItems(input, output, tags);
            var testItems = XDocument.Load(output).Descendants("testItem").ToList();

            // Assert
            AssertItem(testItems, "Iteration2", true);
            AssertItem(testItems, "ProjectTestItem1", true);
            AssertItem(testItems, "ProjectTestItem2", false);
            AssertItem(testItems, "Iteration3", true);
            AssertItem(testItems, "ProjectTestItem3", true);
            AssertItem(testItems, "ProjectTestItem4", false);
            AssertItem(testItems, "ProjectTestItem6", false);
            AssertItem(testItems, "Iteration4", false);
            AssertItem(testItems, "ProjectTestItem5", true);
            AssertItem(testItems, "ProjectTestItem7", false);
        }

        public void AssertItem(IList<XElement> testItems, string itemName, bool enabled)
        {
            var item = testItems.FirstOrDefault(it => it.Attribute("name")?.Value == itemName);
            Assert.NotNull(item);
            var value = item.Attribute("enabled").Value;
            Assert.Equal(enabled, bool.Parse(value));
        }
    }
}