using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Attributes.Solution
{
    public static class ProductValidation
    {
        public static void ValidateProduct(this Product product)
        {
            foreach (var property in product.GetType().GetProperties())
            {
                // for every property loop through all attributes
                foreach (var attribute in property.GetCustomAttributes<StringRange>(false))
                {
                    if (!property.CustomAttributes.Select(at => at.AttributeType)
                        .Contains(typeof(StringRange)))
                    {
                        continue;
                    }
                    var propertyValue = property.GetValue(product) as string;
                    if (propertyValue == null)
                    {
                        throw new InvalidOperationException("Expected string type");
                    }
                    // Do the length check and and raise exception accordingly
                    if (propertyValue.Length > attribute.MaxRange || propertyValue.Length < attribute.MinRange)
                    {
                        throw new ValidationException(
                            $"Value of {property.Name} does not fall within the expected range <{attribute.MinRange}, {attribute.MaxRange}>");
                    }
                    Console.WriteLine($"Validation for {property.Name} passed.");
                }
            }
            // similarly for FieldInfo's
        }
    }
}
