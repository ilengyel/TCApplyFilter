# TCApplyFilter

TestComplete lacks the ability to run a subset of tests based on specifying tags. In the project each test item is tagged appropriately eg, Performance, Serurity, P1. Running this filter will enable only the tests which contain a tag specified on the tool command line.

## Usage

**Options**

    C:\DEV>TCApplyFilter.exe

      -m, --mds       Required. TestComplete test items list file.

      -t, --tags      List of tags to filter in. In OR mode.

      -o, --output    Output file for mutated mds file. Default is to update existing file inline.

      --help          Display this help screen.

      --version       Display version information.


**Example**

## Developer Notes

...