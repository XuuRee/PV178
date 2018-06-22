using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using DataLoader.Model;
using DataLoader.Utils;

namespace DataLoader
{
    public class DataImporter
    {
        #region csv_file_names

        private const string AircraftsCsvFileName = @"Aircrafts.csv";

        private const string AirCrashesCsvFileName = @"AirCrashes.csv";

        private const string CarriersCsvFileName = @"Carriers.csv"; 

        #endregion

        private IList<Carrier> carriers;

        private IList<Aircraft> aircrafts;

        private IList<AirCrash> airCrashes;

        public DataImporter()
        {
            LoadData();
        }

        public IReadOnlyList<Aircraft> ListAllAircrafts()
        {
            return new ReadOnlyCollection<Aircraft>(aircrafts); 
        }

        public IReadOnlyList<Carrier> ListAllCarriers()
        {
            return new ReadOnlyCollection<Carrier>(carriers);
        }

        public IReadOnlyList<AirCrash> ListAllAirCrashes()
        {
            return new ReadOnlyCollection<AirCrash>(airCrashes);
        }

        private void LoadData()
        {
            carriers = ImportFromCsv<Carrier>(CarriersCsvFileName);

            aircrafts = ImportFromCsv<RawAircraft>(AircraftsCsvFileName).ToAircrafts();

            airCrashes = ImportFromCsv<RawAirCrash>(AirCrashesCsvFileName).ToAirCrashes();
        }

        private static IList<T> ImportFromCsv<T>(string filename) where T: new()
        {
            IList<T> items;
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\Resources";
            var filePath = Path.Combine(path, filename);
            using (TextReader reader = File.OpenText(filePath))
            {
                using (var csv = new CsvReader(reader, new CsvConfiguration {HasHeaderRecord = false}))
                {
                    items = csv.GetRecords<T>().ToList();
                }
            }
            return items;
        }
    }
}
