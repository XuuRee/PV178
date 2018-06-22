using System;

namespace Attributes.Solution
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StringRange : Attribute
    {
        public long MinRange { get; set; }

        public long MaxRange { get; set; }
    }
}
