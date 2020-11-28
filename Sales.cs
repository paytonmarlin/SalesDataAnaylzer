using System;

namespace SalesDataAnalyzer
{
    //create a class to initiate with all the headers inside the txt file
    public class Sales
    {
        public int InvoiceNo;

        public int StockCode;

        public string Description;

        public int Quantity;

        public DateTime InvoiceDate;

        public float UnitPrice;

        public int CustomerID;

        public string Country;

    //create constructor that takes in all attributes of the 'Sales' class including DateTime value type
        public Sales(int invoiceNo, int stockCode, string description, int quantity, DateTime invoiceDate, float unitPrice, 
        int customerID, string country)
        {
            InvoiceDate = invoiceDate;
            StockCode = stockCode;
            Description = description;
            Quantity = quantity;
            InvoiceDate = invoiceDate;
            UnitPrice = unitPrice;
            CustomerID = customerID;
            Country = country;
        }
    }
}
