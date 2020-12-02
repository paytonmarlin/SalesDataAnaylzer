using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SalesDataAnalyzer
{
    public static class SalesReport
    {

        public static string GenerateText(List<Sales> salesList)
        {
            string report = "Sales Report\n\n";

            if (salesList.Count() <1)
            {
                report += "No data is available. \n";

                return report;
            }

        //List all the items sold to customers in Austrailia (stockCode and Description).
        report += "Items sold to customers in Australia:";
        var ausItems = from sales in salesList 
                       where sales.Country == "Australia" 
                       select sales;

        if(ausItems.Count() > 0)
        {
            foreach (var sales in ausItems)
            {
                report += "\nStock Code: " + sales.StockCode + "Description: " + sales.Description;
            }
            report.TrimEnd(',');
            report += "\n\n";
        }
        else {
            report = "not available\n";
        }

        //How many individual sales were there? To determine this you have to cout the unique invoice numbers. You should group by invoice number?
        var distinctSales = salesList.Where(s => s.CustomerID != null).GroupBy(s => s.InvoiceNo).Select(grp => grp.FirstOrDefault());; //gets unique numbers by selecting the first value of each group
        var countDistinct = 0;
        if(distinctSales.Count() > 0)
        {
            foreach (var sales in distinctSales)
            {
                countDistinct++;
            }
            report +="\n 2: Number of individual sales: " + countDistinct;
            report.TrimEnd(',');
            report += "\n";
        }
        else{
            report += "not available\n";
            }

        //What is the sales total for invoice number 536365? 
        //Sales total for an invoice will be the sum of (Quantity * UnitPrice) for all products sold in the invoice.
        var salesTotal = from sales in salesList 
                         where sales.InvoiceNo == "536365" 
                         select sales;
        var overallTotal = 0.0; //set default float value
        if(salesTotal.Count() > 0)
        {
            foreach (var sales in salesTotal)
            {
                var quan = sales.Quantity;
                var unit = sales.UnitPrice;
                var singleTotal = quan*unit;
                overallTotal += singleTotal; //add the price total of each sale to the overall total
            }
            //display the decimal total to the report, can use the format method to do so
            report +="\n 3: Sales Total for invoice number 536365: " + "$" + string.Format("{0:0.00}\n", overallTotal);
            report.TrimEnd(',');
            report += "\n";
        }
        else{
            report += "not available\n";
            }
    

        //List the total sales by country
        var groupingCountry = from sales in salesList 
                              group sales by sales.Country into newGroup
                              orderby newGroup.Key //groups them by country for organization
                              select newGroup;
        var amtSales = 0;
        if(groupingCountry.Count() > 0)
        {

            foreach (var salesGroup in groupingCountry)
            {
                report += salesGroup.Key;   //This is the part for the specific country 
                foreach (var sale in salesGroup)
                {
                    amtSales ++;  //foreach country, we want the amount of sales
                }
            report += $": {amtSales}"; 
            report += "\n";
            }
            report.TrimEnd(',');
            report += "\n";
        }
        else{
            report += "not available\n";
            }

        //Which customer has spent the most money during the period?
        var customerMoney = salesList.GroupBy( s => s.CustomerID) //get a group of each customer and get their money spent on the unit price and quantity
                            .Select( x => new {Customer = x.Key, Summation = x.Sum(c => (c.UnitPrice * c.Quantity))});

        if(customerMoney.Count() > 0)
        {
           Dictionary<string, double> customerCost = new Dictionary<string, double>();
        
            foreach(var x in customerMoney)
            {
                customerCost.Add(x.Customer, x.Summation);
            }
            var maxValue = customerCost.Values.Max();
            var keyMatch = customerCost.Keys.FirstOrDefault(s => customerCost[s] == maxValue);
            report += $" 5: Customer {keyMatch} has spent the most money with ${maxValue}\n"; 
        }
        else{
            report += "not available";
        }

        
        



        //What are the total sales to customer 15311?
        var customerSales = from sales in salesList
                            where sales.CustomerID == "15311"
                            select sales;
        var totalSales = 0;
        if(customerSales.Count() > 0)
        {
            foreach (var sales in customerSales)
            {
               totalSales ++;
            }
            report +="\n 6: Total sales for customer 15311: " + totalSales;
            report.TrimEnd(',');
            report += "\n";
        }
        else{
            report += "not available\n";
            }


        //How many units of “HAND WARMER UNION JACK” were sold in the dataset?
        var handWarmer = from sales in salesList
                         where sales.Description == "HAND WARMER UNION JACK"
                         select sales;
        var handUnits = 0;
        if(handWarmer.Count() > 0)
        {
            foreach (var sales in handWarmer)
            {
               handUnits ++;
            }
            report +="\n 7: Unit total for 'HAND WARMER UNION JACK' : " + handUnits;
            report.TrimEnd(',');
            report += "\n";
        }
        else{
            report += "not available\n";
            }



        //What was the total value of the “HAND WARMER UNION JACK” sales in the dataset?
        var handTotal = from sales in salesList
                        where sales.Description == "HAND WARMER UNION JACK"
                        select sales;
        var totalHandSales = 0.0;
        if(handTotal.Count() > 0)
        {
            foreach (var sales in handTotal)
            {
                var quan2 = sales.Quantity;
                var unit2 = sales.UnitPrice;
                var singleTotal = quan2*unit2;
                totalHandSales += singleTotal; //add the price total of each sale to the overall total
            }
            report +="\n 8: Sales Total for 'HAND WARMER UNION JACK': " + "$" + string.Format("{0:0.00}", totalHandSales);
            report.TrimEnd(',');
            report += "\n";
        }
        else{
            report += "not available\n";
            }

        //Which product has the highest UnitPrice (stockCode and Description).?
        var highestUnitPrice = from sales in salesList
                               where sales.UnitPrice > 0
                               orderby sales.UnitPrice descending
                               select sales;
        var highUnit = 0;
        if(highestUnitPrice.Count() > 0)
        {
            foreach (var sales in highestUnitPrice)
            {
                highUnit += 1;
                if (highUnit == 1){
                    report += $"\n 9: Product with the highest unit price \n\t Stock Code:{sales.StockCode} \n\t Description: {sales.Description}";
                }
                else{
                    break;
                }
            }
            report.TrimEnd(',');
            report += "\n";
        }
        else{
            report += "not available\n";
            }


        //What is the total sales for the entire dataset?
        var totalEntireSales = from sales in salesList
                               select sales;
        var totalEntire = 0;
        if(totalEntireSales.Count() > 0)
        {
            foreach (var sales in totalEntireSales)
            {
               totalEntire ++;
            }
            report +="\n 10: Sales Total for entire datasheet : " + totalEntire;
            report.TrimEnd(',');
            report += "\n";
        }
        else{
            report += "not available\n";
            }


        //Which invoice number had the highest sales
        var highSales = (from sales in salesList 
                        group sales by sales.InvoiceNo into highGroup
                        orderby highGroup.Count() descending
                        select highGroup.Key).First();
        if(highSales.Count() > 0)
        {
            report += "\n11: Invoice number with the highest sales: " + highSales;
        }
        
        else{
            report += "not available\n";
        }
        


              
       //Which product sold the most units?
        var productMax = salesList.GroupBy( s => s.Description)
                            .Select( x => new {Product = x.Key, Sold = x.Sum(c => c.Quantity)});

        if(productMax.Count() > 0)
        {
           Dictionary<string, int> products = new Dictionary<string, int>();

        foreach (var x in productMax)
        {
            products.Add(x.Product, x.Sold);
        }

        var maxQuantity = products.Values.Max();
        var maxProduct = products.Keys.FirstOrDefault(s => products[s] == maxQuantity);
        report += $"\n\n12: Product {maxProduct} has sold the most units with {maxQuantity} units";
 
        }
        else{
            report += "not available";
        }
        
        

        return report;
        }
    }







}