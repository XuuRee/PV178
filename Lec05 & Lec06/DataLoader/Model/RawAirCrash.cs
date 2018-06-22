using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader.Model
{
    public class RawAirCrash
    {
        /// <summary>
        /// Unique identifier of the air crash
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Time of the air crash
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Description of the air crash location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Unique identifier of the airline company
        /// to which the involved aircraft belongs
        /// </summary>
        public string CarrierCode { get; set; }

        /// <summary>
        /// Start point and destination of the flight
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Unique identifier of involved aircraft
        /// </summary>
        public string AircraftType { get; set; }

        /// <summary>
        /// Number of people who were aboard the aircraft
        /// default value is -1 (unknown aboard count)
        /// </summary>
        public int Aboard { get; set; }

        /// <summary>
        /// Number of people who died in the accident
        /// default value is -1 (unknown fatalities count)
        /// </summary>
        public int Fatalities { get; set; }

        /// <summary>
        /// Information about the air crash
        /// </summary>
        public string Description { get; set; }
    }
}
