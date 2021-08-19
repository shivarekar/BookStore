using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BookStore
{
    class Program
    {
        static void Main(string[] args)
        {

            IDictionary<string, int> order = new Dictionary<string, int>();

            order.Add("Unsolved murders", 1);
            order.Add("A Little Love Story", 1);
            order.Add("Heresy", 1);
            order.Add("Jack the Ripper", 1);
            order.Add("The Tolkien Years", 1);



            StreamReader sr = new StreamReader("BookDB.json");
            string jsonString = sr.ReadToEnd();
            BookStore bookStore = JsonConvert.DeserializeObject<BookStore>(jsonString);


            double total = 0;
            double gst = 10;
            double deliveryFee = 5.95;
            Console.WriteLine("Your order is as below:");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Book->Quantity");
            Console.WriteLine("----------------------------");
            foreach (KeyValuePair<string, int> item in order)
            {
                Console.WriteLine(item.Key + "->" + item.Value);
                foreach (var category in bookStore.Categories)
                {
                    var discount = category.Discount / 100;
                    var book = category.Books.FirstOrDefault(x => x.Title.Contains(item.Key));
                    if (book != null)
                    {
                        var unitPrice = book.UnitPrice;
                        total += (unitPrice * item.Value) - (unitPrice * item.Value * discount);
                    }
                }
            }

           
            Console.WriteLine("----------------------------");
            Console.WriteLine("Sub-Total------" + total + "(AUD)");

            total += total * (gst / 100);

            Console.WriteLine("GST------------" + (gst / 100).ToString("P", CultureInfo.InvariantCulture));
            if (total < 20.00)
            {
                total += deliveryFee;
                Console.WriteLine("Delivery Fee---"+ deliveryFee + "(AUD)");
            }
            Console.WriteLine("Total----------" + total + "(AUD)");

        }
    }
}
