using System.Collections.Generic;
using DataLoader;
using DataLoader.Model;

namespace LINQ
{
    public static class DataContext
    {
        public static IReadOnlyList<Carrier> Carriers { get; private set; }

        public static IReadOnlyList<Aircraft> Aircrafts { get; private set; }

        public static IReadOnlyList<AirCrash> AirCrashes { get; private set; }

        static DataContext()
        {
            Initialize();
        }

        private static void Initialize()
        {
            var importer = new DataImporter();
            Carriers = importer.ListAllCarriers();
            Aircrafts = importer.ListAllAircrafts();
            AirCrashes = importer.ListAllAirCrashes();
        }
    }
}
