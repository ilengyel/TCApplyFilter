namespace TCApplyFilter
{
    using System.Collections.Generic;

    public interface IIncludeFilter<T>
    {
        IEnumerable<string> FilterItems(string input, string output, IList<T> includeItems);

        IEnumerable<string> ListItems(string input);

        IEnumerable<string> ListTags(string input);
    }
}