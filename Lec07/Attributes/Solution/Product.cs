using System.ComponentModel.DataAnnotations;

namespace Attributes.Solution
{
    public class Product
    {
        [StringRange(MinRange = 3, MaxRange = 64)]
        public string Name{ get;  }

        [MinLength(2), MaxLength(32)]
        public string Manufacturer { get; }

        public Product(string manufacturer, string name)
        {
            Manufacturer = manufacturer;
            Name = name;
        }
    }
}
