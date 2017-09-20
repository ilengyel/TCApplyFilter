namespace TCApplyFilter
{
    using System;
    using CommandLine;

    public abstract class Command
    {
        [Option('m', "mds", Required = true, HelpText = "TestComplete test items list file.")]
        public string MdsFile { get; set; }

        public int Run()
        {
            try
            {
                Handle();
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return 1;
            }
        }

        protected abstract void Handle();
    }
}
