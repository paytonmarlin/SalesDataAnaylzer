using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace SalesDataAnalyzer
{
    //create a class to load the data from the csv file and put it into a list
    public static class SalesLoader
    {
        //specifies that there are 8 'fields' in the list
        private static int NumItemsInRow = 8;

        //make a new list and load the data file path (input by user)
        public static List<Sales> Load(string salesDataFilePath){
            List<Sales> salesList = new List<Sales>();

        //iterate throught the StreamReader for each index, but skips the first line with all the headers
            try
            {
                using (TextFieldParser reader = new TextFieldParser(new StreamReader(salesDataFilePath)))
                {

                    reader.HasFieldsEnclosedInQuotes = true;
                    reader.SetDelimiters(",");

                    int lineNumber = 0;
                    while (!reader.EndOfData)
                    {
                        string[] lines = reader.ReadFields();
                        lineNumber++;
                        if (lineNumber == 1) continue;

                        //var values = line.Split(","); //splits into tabs, not commas
                        foreach(string line in lines)
                        {
                            string invoiceNo = (lines[0]);
                            string stockCode = (lines[1]);
                            string description = (lines[2]);
                            int quantity = Int32.Parse(lines[3]);
                            DateTime invoiceDate = DateTime.Parse(lines[4]);
                            float unitPrice = float.Parse(lines[5]);
                            string customerID = (lines[6]);
                            string country = (lines[7]);

                            Sales sales = new Sales(invoiceNo, stockCode, description, quantity, invoiceDate, unitPrice, customerID, country);
                            salesList.Add(sales);
                        }
                       

                    //gives error is the csv file has more than 8 fields
                        // if (values.Length == 9)
                        // {
                        //     var twoCommas = string.Concat(values[2] + values[3]);
                        //     var stringTwoCommas = Convert.ToString(twoCommas);
                        //     line.Replace(values[2], stringTwoCommas);
                        //     line.Replace(values[3], "");
                            
                        // }
                        // if (values.Length != NumItemsInRow)
                        // {
                        //     throw new Exception($"Row {lineNumber} contains {values.Length} values. It should contain {NumItemsInRow}.");
                        // }
                    
                    //now we can put the fields and convert them to their desired value types (int, float, string DateTime)
                        // try
                        // {
                        //     int invoiceNo = Int32.Parse(values[0]);
                        //     string stockCode = (values[1]);
                        //     string description = (values[2]);
                        //     int quantity = Int32.Parse(values[3]);
                        //     DateTime invoiceDate = DateTime.Parse(values[4]);
                        //     float unitPrice = float.Parse(values[5]);
                        //     int customerID = Int32.Parse(values[6]);
                        //     string country = (values[7]);

                        // //create a variable of reference type Sales that takes in the new variables
                        //     Sales sales = new Sales(invoiceNo, stockCode, description, quantity, invoiceDate, unitPrice, customerID, country);
                        //     salesList.Add(sales);
                        // }
                        // catch (FormatException e)
                        // {
                        //     throw new Exception($"Row {lineNumber} contains invalid data. ({e.Message})");
                        // }
                    }
                }
            } catch (Exception e){
                throw new Exception($"Unable to open {salesDataFilePath} ({e.Message}).");
            }

            return salesList;
        }
    }
}
