using System;

namespace DataLoader.Model
{
    public class Aircraft
    {
        /// <summary>
        /// unique aircraft type (Airbus A319, ...)
        /// </summary>
        public string AircraftType { get; set; }

        /// <summary>
        /// name of the aircraft manufacturer
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// year (and month) of the first flight
        /// </summary>
        public DateTime FirstFlight { get; set; }

        /// <summary>
        /// number of made aircrafts,
        /// -1 stands for default value
        /// </summary>
        public int UnitsBuilt { get; set; }

        public override string ToString()
        {
            return AircraftType;
        }
    }
}
