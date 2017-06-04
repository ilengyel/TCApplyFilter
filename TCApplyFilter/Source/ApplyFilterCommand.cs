namespace TCApplyFilter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandLine;

    public class ApplyFilterCommand
    {
        [Option('m', "mds", Required = true, HelpText = "TestComplete test items list file.")]
        public string MdsFile { get; set; }

        [Option('t', "tags", HelpText = "List of tags to filter in. In OR mode.")]
        public IEnumerable<string> Tags { get; set; }

        [Option('o', "output", HelpText = "Output file for mutated mds file. Default is to update existing file inline.")]
        public string OutputFile { get; set; }

        public int Run()
        {
            try
            {
                var targetFile = OutputFile ?? MdsFile;
                var filter = new TestCompleteFilter();
                filter.FilterItems(MdsFile, targetFile, Tags.ToList());
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}
