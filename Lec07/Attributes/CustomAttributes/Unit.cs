using System;

namespace Attributes.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class Unit : Attribute
    {
        public UnitType UnitType { get; }

        public Unit(UnitType unit)
        {
            UnitType = unit;
        }
    }
}
