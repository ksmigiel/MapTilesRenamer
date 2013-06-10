using System;
using System.IO;
using System.Text;

namespace MapTilesRenamer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                StringBuilder message = new StringBuilder();
                message.AppendLine("Please use MapTilesRenamer.exe [source extension] [destination extension] [[path]]");
                message.AppendLine("and run from maps main catalog.");
                message.AppendLine("e.g MapTilesRenamer.exe .png .png.TILE");
                message.Append("from [puthere]\\z\\x\\y.png");
                Console.WriteLine(message);
                Console.ReadKey();
                Environment.Exit(0);
            }
            if (args.Length < 2 || args.Length > 3)
            {
                Console.WriteLine("Wrong parameters");
                Console.ReadKey();
                Environment.Exit(0);
            }
            SlippyTMSRenamer tileRenamer = new SlippyTMSRenamer(args[0], args[1], args[2]);
            tileRenamer.RenameAll();
            Console.WriteLine("DONE!");
            Console.ReadKey();
        }
    }
}
