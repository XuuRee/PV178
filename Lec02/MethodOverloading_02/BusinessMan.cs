using System;

namespace OperatorOverloading
{
    public class BusinessMan : IComparable<BusinessMan>
    {
        public string Name { get; set; }
        public decimal ValueOfCar { get; set; }
        public decimal ValueOfHouse { get; set; }
        public decimal ValueOfBuisness { get; set; }

        public int CompareTo(BusinessMan other)
        {
            decimal thisValue = ValueOfCar + ValueOfHouse + ValueOfBuisness;
            decimal otherValue = other.ValueOfCar + other.ValueOfHouse + other.ValueOfBuisness;
            return thisValue.CompareTo(otherValue);
        }

        public static bool operator >(BusinessMan bman1, BusinessMan bman2)
        {
            return bman1.CompareTo(bman2) > 0;
        }

        public static bool operator <(BusinessMan bman1, BusinessMan bman2)
        {
            return bman1.CompareTo(bman2) < 0;
        }

        public static bool operator <=(BusinessMan bman1, BusinessMan bman2)
        {
            return !(bman1 > bman2);
        }

        public static bool operator >=(BusinessMan bman1, BusinessMan bman2)
        {
            return !(bman1 < bman2);
        }
    }
}
