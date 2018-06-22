
namespace DataLoader.Model
{
    public class Carrier
    {
        /// <summary>
        /// contains unique 3-character carrier code (CSA, UAE, ...)
        /// </summary>
        public string Code { get;  set; }

        /// <summary>
        /// contains carrier name (Czech Airlines, Emirates, ...)
        /// </summary>
        public string Name { get;  set; }

        public override string ToString()
        {
            return $"[{Code}] {Name}";
        }
    }
}
