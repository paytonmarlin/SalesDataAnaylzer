using System;

namespace SalesDataAnalyzer
{
    //create a class to initiate with all the headers inside the txt file
    public class Sales
    {
        public string InvoiceNo;

        public string StockCode;

        public string Description;

        public int Quantity;

        public DateTime InvoiceDate;

        public float UnitPrice;

        public string CustomerID;

        public string Country;

    //create constructor that takes in all attributes of the 'Sales' class including DateTime value type
        public Sales(string invoiceNo, string stockCode, string description, int quantity, DateTime invoiceDate, float unitPrice, 
        string customerID, string country)
        {
            InvoiceNo = invoiceNo;
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
