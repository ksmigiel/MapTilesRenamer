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
                message.AppendLine("Please use MapTilesRenamer.exe [source extension] [destination extension]");
                message.AppendLine("and run from maps main catalog.");
                message.AppendLine("e.g MapTilesRenamer.exe .png .png.TILE");
                message.Append("from [puthere]\\z\\x\\y.png");
                Console.WriteLine(message);
                Console.ReadKey();
                Environment.Exit(0);
            }
            if (args.Length < 2)
            {
                Console.WriteLine("Wrong parameters");
                Console.ReadKey();
                Environment.Exit(0);
            }
            TilesRenamer tileRenamer = new TilesRenamer(args[0], args[1]);
            tileRenamer.RenameAll();
            Console.WriteLine("DONE!");
            Console.ReadKey();
        }
    }
}
