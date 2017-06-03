namespace TCApplyFilter
{
    using CommandLine;

    public static class Program
    {
        /// <summary>
        /// Main method for program
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
            => Parser.Default.ParseArguments<ApplyFilterCommand>(args)
                .WithParsed(c => c.Run());
    }
}
