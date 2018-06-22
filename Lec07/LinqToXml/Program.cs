using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using LinqToXml.SampleData;

namespace LinqToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            // Reseni ukolu, zadani viz Lab07_Tasks
            // Solution.Tasks();

            // get the root element
            var rootElement = XElement.Load(Paths.CustomersAndOrders);

            // root element can be obtained also via XDocument.Load(...) method
           // var rootElement = XDocument.Load(Paths.CustomersAndOrders).Root;


            // get all child elements
            var rootXmlChildren = rootElement.Elements().ToList();
            
            foreach (var rootXmlChild in rootXmlChildren)
            {
                Console.WriteLine(rootXmlChild);
            }

            // get all children from customers element 
            var customers = rootElement.Element("Customers")?.Elements().ToList() ?? new List<XElement>();
            
            foreach (var customer in customers)
            {
                // access specific attribute within the customer element
                Console.WriteLine(customer.Attribute("CustomerID")?.Value);
            }

            // LinqToXml sample: Get all elements with "Manager" contact title
            var managerElements = customers
                .Where(customer => customer.Element("ContactTitle")?.Value.Contains("Manager") ?? false)
                .ToList();

            // LinqToXml sample: Get all contact names from managerElements
            var managerContactNamesList = managerElements
                .Select(customer => customer.Element("ContactName"))
                .Select(element => element.Value)
                .ToList();


            // LinqToXml sample: Get all nested "City" elements from managerElements
            // Note that not all city elements are present (Howard Snyder has no city defined)
            var managerCities = managerElements
                .Descendants("City")
                .Select(c => c.Value)
                .ToList();

            // LinqToXml sample: Get manager postal codes (in descending order)
            var orderedPostalCodes = managerElements
                .SelectMany(customer => customer.Descendants("PostalCode"))
                .Select(postalCodeElement => postalCodeElement.Value)
                .OrderByDescending(postalCode => postalCode)
                .ToList();



            // LinqToXml sample: Get addresses based on their content (take addresses with city element only)
            var addressesWithCities = managerElements
                .SelectMany(customer => customer.Descendants("Address"))
                .Where(postalCodeElement => postalCodeElement.ElementsAfterSelf().First().Name.LocalName.Equals("City"))
                .ToList();

            // create Xml
            var srcTree = new XElement("Root",
                new XElement("number", 1),
                new XElement("number", 2),
                new XElement("number", 3),
                new XElement("number", 4),
                new XElement("number", 5)
            );
            var xmlTree = new XElement("Root");

            // add elements
            xmlTree.Add(new XElement("EvenNumbers"));
            var evenNumbersElement = xmlTree.Element("EvenNumbers");
            evenNumbersElement?.SetAttributeValue("Count", 2);
            evenNumbersElement?.Add(srcTree.Elements().Where(element => int.Parse(element.Value) % 2 == 0));
            Console.WriteLine("Created xml tree:");
            Console.WriteLine(xmlTree + Environment.NewLine);

            // update elements
            evenNumbersElement?.SetElementValue("number", 6);
            evenNumbersElement?.Elements().Last().SetValue(8);
            Console.WriteLine("Edited xml tree:");
            Console.WriteLine(xmlTree + Environment.NewLine);

            // remove elements
            evenNumbersElement?.Elements().Remove();
            evenNumbersElement?.SetAttributeValue("Count", 0);
            Console.WriteLine("Cleared xml tree:");
            Console.WriteLine(xmlTree + Environment.NewLine);
            Console.ReadKey();
        }
    }
}
