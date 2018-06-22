using Attributes.CustomAttributes;

namespace Attributes
{
    public class Route
    {
        public string From { get; }
        
        public string To { get; }

        [Unit(UnitType.Meters)]
        private readonly long distance;

        public Route(string from, string to, long distance)
        {
            From = from;
            To = to;
            this.distance = distance;
        }
    }
}
