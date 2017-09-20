namespace TCApplyFilter
{
    using System;
    using CommandLine;

    [Verb("listtags", HelpText = "List the set of tags.")]
    public class ListTagsCommand : Command
    {
        /// <inheritdoc />
        protected override void Handle()
        {
            var filter = new TestCompleteFilter();
            foreach (var entry in filter.ListTags(MdsFile))
            {
                Console.WriteLine(entry);
            }
        }
    }
}
