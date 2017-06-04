namespace TCApplyFilter
{
    using System.Collections.Generic;

    public interface IIncludeFilter<T>
    {
        void FilterItems(string input, string output, IList<T> includeItems);
    }
}