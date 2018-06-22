using System;
using System.Collections.Generic;
using System.Linq;
using DataLoader.Model;

namespace DataLoader.Utils
{
    public static class Converters
    {
        public static IList<Aircraft> ToAircrafts(this IList<RawAircraft> rawAircrafts)
        {
            return rawAircrafts.Select(rawAircraft => new Aircraft
            {
                AircraftType = rawAircraft.AircraftType,
                FirstFlight = ParseAirCraftDateTime(rawAircraft.FirstFlight),
                Manufacturer = rawAircraft.Manufacturer,
                UnitsBuilt = rawAircraft.UnitsBuilt
            }).ToList();
        }

        public static List<AirCrash> ToAirCrashes(this IList<RawAirCrash> rawAirCrashes)
        {
            return rawAirCrashes.Select(rawAirCrash => new AirCrash
            {
                Id = rawAirCrash.Id,
                Date = ParseAirCrashDateTime(rawAirCrash.Date, rawAirCrash.Id),
                Location = rawAirCrash.Location,
                CarrierCode = rawAirCrash.CarrierCode,
                Route = rawAirCrash.Route,
                AircraftType = rawAirCrash.AircraftType,
                Aboard = rawAirCrash.Aboard,
                Fatalities = rawAirCrash.Fatalities,
                Description = rawAirCrash.Description
            }).ToList();
        }


        #region DateTimeParsers

        /// <summary>
        /// Parses correct aircraft firstFlight date from string dateTime with only 2-digit year
        /// </summary>
        /// <param name="dateTimeString"></param>
        /// <returns></returns>
        public static DateTime ParseAirCraftDateTime(string dateTimeString)
        {
            var data = dateTimeString.Split(' ');
            var dateData = data[0].Split('/');
            var timeData = data[1].Split(':');
            if (dateData[0].Length == 2)
            {
                if (dateData[0].Equals("01"))
                {
                    dateData[0] = "00" + dateData[0];
                }
                else
                {
                    dateData[0] = "19" + dateData[0];
                }               
            }
            return new DateTime(Math.Max(int.Parse(dateData[0]), 1), int.Parse(dateData[1]),
                int.Parse(dateData[2]), int.Parse(timeData[0]), int.Parse(timeData[1]), int.Parse(timeData[2]));
        }

        /// <summary>
        /// Parses correct airCrash date from string dateTime with only 2-digit year
        /// </summary>
        /// <param name="dateTimeString"></param>
        /// <returns></returns>
        public static DateTime ParseAirCrashDateTime(string dateTimeString, int id)
        {
            var data = dateTimeString.Split(' ');
            var dateData = data[0].Split('/');
            var timeData = data[1].Split(':');
            if (id < 4686)
            {
                dateData[0] = "19" + dateData[0];
            }
            else
            {
                dateData[0] = "20" + dateData[0];
            }
            return new DateTime(Math.Max(int.Parse(dateData[0]), 1), int.Parse(dateData[1]),
                int.Parse(dateData[2]), int.Parse(timeData[0]), int.Parse(timeData[1]), int.Parse(timeData[2]));
        }

        #endregion

    }
}
