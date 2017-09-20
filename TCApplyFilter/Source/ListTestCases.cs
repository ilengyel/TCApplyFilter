namespace TCApplyFilter
{
    using System;
    using CommandLine;

    [Verb("list", HelpText = "List the set of test cases and status.")]
    public class ListTestCases : Command
    {
        protected override void Handle()
        {
            var filter = new TestCompleteFilter();
            foreach (var entry in filter.ListItems(MdsFile))
            {
                Console.WriteLine(entry);
            }
        }
    }
}
