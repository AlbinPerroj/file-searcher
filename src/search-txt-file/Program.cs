using search_txt_file.Handlers;
using System.Diagnostics;

namespace search_txt_file
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 2)
            {
                Console.WriteLine("cmd environment arguments missing");
                return;
            }

            string inputFileName = args[0];
            string resultFileName = args[1];
            string methodFlag;

            if(args.Length >= 3)
            {
                methodFlag = args[2];
            }
            else
            {
                methodFlag = "0";
            }


            TxtFileSearcher txtFileSearcher = new TxtFileSearcher(inputFileName, resultFileName);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            bool status = false;
            if (methodFlag == "0")
            {
                status = txtFileSearcher.findMinAndMaxValueForEachVarible();
            }
            else if(methodFlag=="1")
            {
                status = txtFileSearcher.findMinAndMaxValue();
            }

            if (status)
            {
                txtFileSearcher.writeResultToFile();
            }

            stopwatch.Stop();

            Console.WriteLine("Execution time is: " + stopwatch.ElapsedMilliseconds + " milliseconds.");
        }
    }
}