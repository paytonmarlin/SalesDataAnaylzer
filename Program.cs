using System;
using System.Collections.Generic;

namespace SalesDataAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            //program takes in arguments and generates error if files are not found, etc.
            if(args.Length != 2) {
                Console.WriteLine("Music <music_text_file_path> <report_file_path");
                Environment.Exit(1);
            }

            string salesDataFilePath = args[0];
            string reportFilePath = args[1];

            List<Sales> salesList = null;
            try
            {
                salesList = SalesLoader.Load(salesDataFilePath);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Environment.Exit(2);
            }
        }
    }
}
