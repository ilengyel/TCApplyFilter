namespace TCApplyFilter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandLine;

    [Verb("apply", HelpText = "Apply the filter by enabling test cases which have tags defined by the supplied list, and disabling the rest.")]
    public class ApplyFilterCommand : Command
    {
        [Option('t', "tags", HelpText = "List of tags to filter in. In OR mode.")]
        public IEnumerable<string> Tags { get; set; }

        [Option('o', "output", HelpText = "Output file for mutated mds file. Default is to update existing file inline.")]
        public string OutputFile { get; set; }

        protected override void Handle()
        {
            var targetFile = OutputFile ?? MdsFile;
            var filter = new TestCompleteFilter();
            var tags = Tags
                .Select(t => t.Trim(','))
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .ToList();
            var enabledList = filter.FilterItems(MdsFile, targetFile, tags);
            foreach (var testItem in enabledList)
            {
                Console.WriteLine(testItem);
            }
        }
    }
}
