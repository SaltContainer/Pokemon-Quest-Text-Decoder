using System;
using System.IO;
using System.Text;

namespace mPqTextAssetDecoder
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 3)
            {
                PrintUsage();
                return -1;
            }

            try
            {
                Run(args);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return -1;
            }
        }

        static void Run(string[] args)
        {
            var data = File.ReadAllBytes(args[0]);
            MessageData md = new MessageData();
            md.Decode(data);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md.messages.Count; i++)
            {
                sb.AppendFormat("{0}\n", md.messages[i]);
            }

            var decoded = sb.ToString();
            File.WriteAllText(args[1], decoded);

            var reencoded = md.Encode(MessageData.Coded.DATA_NO_CODED);
            File.WriteAllBytes(args[2], reencoded);
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: mPqTextAssetDecoder input output reencoded");
            Console.WriteLine("  input: TextAsset bytes file");
            Console.WriteLine("  output: Output file path for decoded file");
            Console.WriteLine("  reencoded: Output file path for reencoded file");
        }
    }
}
