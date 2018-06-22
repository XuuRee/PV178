using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Attributes.CustomAttributes;
using Attributes.Solution;

namespace Attributes
{
    class Program
    {
        static void Main(string[] args)
        {
            RunPredefinedAttributesUsage();
            Console.ReadKey();
            RunCustomAttributesUsage();
            Console.ReadKey();
        }

        private static void RunPredefinedAttributesUsage()
        {
            // Obsolete
            var sum = Add(1, 2);

            // Serializable
            var customer = new Customer("Jon Doe", 20);
            IFormatter formatter = new BinaryFormatter();
            var buffer = new byte[512];
            using (Stream stream = new MemoryStream(buffer))
            {
                formatter.Serialize(stream, customer);
            }
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(buffer));

            // There are also other predefined attributes like Conditional, DllImport, ...
        }

        [Obsolete("Will be removed in next version.")]
        public static int Add(int a, int b)
        {
            return a + b;
        }

        private static void RunCustomAttributesUsage()
        {
            var route = new Route("Brno", "Olomouc", 78400);
            CheckUnits(route);

            // Task - create custom attribute
            //
            // I.   Convert class StringRangeAttribute to attribute 
            //      which specifies string min and max length, ensure
            //      that this attribute can only be used on properties
            //      or fields.
            // 
            // II.  Add annotations for this attribute to Product class,
            //      use arbitrary values for min/max length.
            // 
            // III. Create extension method ValidateProduct within static
            //      ProductValidation class which ensures, that values of
            //      all anotated properties falls within expected range.
            // 
            // IV.  Uncomment the method call below and test your code.


            var product = new Product("Samsung", "Galaxy S8");
            product.ValidateProduct();

            var results = new List<ValidationResult>();
            var vc = new ValidationContext(product, null, null);
            var success = Validator.TryValidateObject(product, vc, results, true);
            if (success)
            {
                Console.WriteLine($"Validation for {nameof(Product.Manufacturer)} passed.");
            }
            else
            {
                var errorMsgBuilder = new StringBuilder($"Validation for {nameof(Product.Manufacturer)} failed:" + Environment.NewLine);
                foreach (var result in results)
                {
                    errorMsgBuilder.Append(result.ErrorMessage);
                }
                Console.WriteLine(errorMsgBuilder);
            }
        }

        private static void CheckUnits(Route route)
        {
            foreach (var field in typeof(Route).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                // for every property loop through all attributes
                foreach (var attribute in field.GetCustomAttributes<Unit>(false))
                {
                    if (!field.CustomAttributes.Select(at => at.AttributeType)
                        .Contains(typeof(Unit)))
                    {
                        continue;
                    }
                    Console.WriteLine($"Distance from {route.From} to {route.To} is {field.GetValue(route)} {attribute.UnitType}");                   
                }
            }
            // similarly for FieldInfo's
        }
    }
}
