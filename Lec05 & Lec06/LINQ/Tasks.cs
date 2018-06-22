using System;
using System.Collections.Generic;
using System.Linq;
using DataLoader.Model;
using LINQ.HelperMethods;

namespace LINQ
{
    public static class Tasks
    {
        public static void Task01()
        {
            //vypiste vsetky lietadla, ktorych sa vyrobilo viac ako 5000
            var aircraftUnitsMoreThan5000 = DataContext.Aircrafts.
                Where(c => c.UnitsBuilt > 5000).
                ToList();
           LinqHelperMethods.WriteResult(aircraftUnitsMoreThan5000, "Aircrafts with more than 5000 units built");


            //vypiste vsetky havarie, ktore sa stali na mieste "Newark, New Jersey" a mali menej ako 5 pasazierov
            var crashesInNewarkAndLessThan5Passengers = DataContext.AirCrashes.
                Where(c => c.Location == "Newark" || c.Location == "New Jersey" && c.Aboard < 5)    // c.Location.Equals("Newark, New Jersey")
                .ToList(); 
            LinqHelperMethods.WriteResult(crashesInNewarkAndLessThan5Passengers, "Crashes in Newark, New Jersey with less than 5 passengers");


            // vypiste vsetky kody spolocnosti, ktore zacinaju cislicou 1, zoradene podla abecedy
            var carrierOrderedCodes = DataContext.Carriers.
                Where(c => c.Code.StartsWith("1")).
                OrderByDescending(c => c.Code).
                ToList();
            LinqHelperMethods.WriteResult(carrierOrderedCodes, "Ordered carrier codes beginning with 1");


            // vypiste prvu havariu pri ktorej zomrelo 10 ludi
            var crashWith10Fatalities = DataContext.AirCrashes.
                First(c => c.Fatalities == 10);
            LinqHelperMethods.WriteResult(crashWith10Fatalities, "First crash with 10 fatalities");


            // vypiste tretiu havariu pri ktorej zomrelo 10 ludi
            var thirdCrashWith10Fatalities = DataContext.AirCrashes.
                Where(c => c.Fatalities == 10).
                ElementAt(2);
            LinqHelperMethods.WriteResult(thirdCrashWith10Fatalities, "Third crash with 10 fatalities");


            // vypiste priemerny pocet pasazierov lietadiel typu "Lockheed Vega", ktore havarovali
            var avgPassengersOnLockheedVega = DataContext.AirCrashes.
                Where(c => c.AircraftType == "Lockheed Vega").
                Average(c => c.Aboard);
            LinqHelperMethods.WriteResult(avgPassengersOnLockheedVega, "Average passengers on Lockheed Vega which crashed");
        }

        public static void Task02()
        {
            // vypiste pocet lietadiel, ktorych vyrobca bola firma "Lockheed Corporation"
            var aircraftsByLockheedCorporation = DataContext.Aircrafts.
                Count(c => c.Manufacturer.Equals("Lockheed Corporation"));
            LinqHelperMethods.WriteResult(aircraftsByLockheedCorporation, "Lockheed aircrafts");


            // Zjistete celkovy pocet evidovanych obeti, nezapomente
            // vyloucit defaultni hodnoty (-1) z dotazu
            var totalFatalitiesCount = DataContext.AirCrashes.
                Where(c => c.Fatalities > 0).
                Sum(c => c.Fatalities);
            LinqHelperMethods.WriteResult(totalFatalitiesCount, "Total fatalities recorded");


            // najdete nejcetnejsi letadlo (typ s nejvice vyrobenymi kusy),
            // jehoz prvni let se uskutecnil mezi roky 1960 az 1990 vcetne
            var mostCommonAircraft = DataContext.Aircrafts.
                Where(c => c.FirstFlight.CompareTo(1960) > 0 && c.FirstFlight.CompareTo(1990) < 0).
                OrderBy(c => c.UnitsBuilt).
                First();
            LinqHelperMethods.WriteResult(mostCommonAircraft, "Most common aircraft");
            

            // zjistete roky, kdy doslo k padu letadel na nasem uzemi (czechoslovakia), 
            // zajistete, aby porovnani retezcu probehlo jako case insensitive
            var airCrashesInCzechoslovakia = DataContext.AirCrashes.
                Where(c => c.Location.ToLower().Contains("czechoslovakia")).
                Select(c => c.Date.Year).
                ToList();
                LinqHelperMethods.WriteResult(airCrashesInCzechoslovakia, "Years of aircrashes in CzechoSlovakia");


            // rozdelte udaje o leteckych katastrofach dle
            // vyctoveho typu AirCrashClassification (viz popis), 
            // Tip: vyuzijte predpripravene extension metody ClassifyAircrash,
            // kterou naleznete v LinqHelperMethods
            var airCrashesClasification = DataContext.AirCrashes.
                GroupBy(c => LinqHelperMethods.ClassifyAircrash(c.Fatalities)).
                ToList();
            LinqHelperMethods.WriteResult(airCrashesClasification, "Air crashes classification");


            // vyberte vsechny letadla, jejiz prvni let se uskutecnil v prestupnem roce
            var firstFlightInLeapYear = DataContext.Aircrafts.
                Where(c => DateTime.IsLeapYear(c.FirstFlight.Year));
            LinqHelperMethods.WriteResult(firstFlightInLeapYear, "Aircrafts with first flight in leap year");
        }

        public static void Task03()
        {
            // vypiste minimalnu hodnotu vyrobenych kusov z prvych 10 lietadiel, ktorych prvy let sa uskutocnil
            // od roku 1964
            
            var minUnitsBuiltOfFirst10AircraftsFrom1964 = DataContext.Aircrafts.
                Where(c => c.FirstFlight.CompareTo(1964) > 0).
                //Select(c => c.UnitsBuilt).
                Take(10).
                Min(c => c.UnitsBuilt);
            LinqHelperMethods.WriteResult(minUnitsBuiltOfFirst10AircraftsFrom1964, "Minimal units built of first 10 aircrafts from 1964");


            // Nejdrive sestupne seradte nazvy vyrobcu letadel (Manufacturer), 
            // nasledne vzestupne vypiste typ prvnich deseti letadel
            var aircraftCodesFrom10LastManufacturers = DataContext.Aircrafts.
                OrderBy(c => c.Manufacturer).Select(c => c.Manufacturer);

            //LinqHelperMethods.WriteResult(aircraftCodesFrom10LastManufacturers, "Aircraft codes from 10 last manufacturers");


            // vypiste vsechny lokace, kde havarovalo nejake letadlo od vyrobce Boeing
            var boeingAircrashLocations = DataContext.Aircrafts.
                Where(c => c.Manufacturer.Contains("Boeing")).
                Join(DataContext.AirCrashes,
                    aircraft => aircraft.AircraftType,
                    crash => crash.AircraftType,
                    (aircraft, crash) => crash).
                Select(Aircrash => Aircrash.Location).
                ToList();
            LinqHelperMethods.WriteResult(boeingAircrashLocations, "Boeing aircrash locations");


            // vypiste pocet obeti a plne nazvy dopravcu u 10
            // nejhorsich leteckych nehod (majici nejvetsi pocet obeti)
            var worstAirCrashes = DataContext.AirCrashes.
                Join(DataContext.Carriers,
                    crash => crash.CarrierCode,
                    carrier => carrier.Code,
                    (crash, carrier) => new { carrier.Name, crash.Fatalities }).
                OrderByDescending(c => c.Fatalities).
                Take(10).
                ToList();
            LinqHelperMethods.WriteResult(worstAirCrashes, "Worst 10 air crashes");


            // vypiste vyrobce letadel, ktera havarovala s vice
            // jak 100 lidmi na palube, nezapomente odfiltrovat duplicity
            var manufacturersInvolvedInSevereAirCrashes = DataContext.AirCrashes.
                Join(DataContext.Carriers,
                    crash => crash.CarrierCode,
                    carrier => carrier.Code,
                    (crash, carrier) => new { crash.Fatalities, carrier.Name }).
                Where(c => c.Fatalities > 100).
                Select(c => c.Name).
                Distinct().
                ToList();
            LinqHelperMethods.WriteResult(manufacturersInvolvedInSevereAirCrashes, "Manufacturers involved in tragic air crashes");
        }

        public static void Task04()
        {
            // vypiste plne nazvy leteckych spolecnosti, ktere 
            // pouzivaji alespon jedno z 5 nejbeznejsich letadel 
            /*
            var carriersWithCommonAircrafts =
                DataContext.Aircrafts.
                OrderByDescending(c => c.UnitsBuilt).
                Take(5).
                Join(DataContext.AirCrashes,
                    aircraft => aircraft.AircraftType,
                    crash => crash.AircraftType,
                    (aircraft, crash) => new { crash.CarrierCode }).
                Join(DataContext.Carriers,
                    anon => anon.CarrierCode,
                    carrier => carrier.Name,
                    (anon, carrier) => new { carrier.Name }).
                Distinct().
                ToList();
            LinqHelperMethods.WriteResult(carriersWithCommonAircrafts, "Carriers with 5 common aircrafts ");
          

            // vypiste plne nazvy leteckych spolecnosti, ktere 
            // pouzivaji alespon jedno z 5 nejbeznejsich letadel
            //  + vypiste i 5 nejpouzivanejsich letadel
            var carriersAndCommonAircrafts = DataContext.Aircrafts.OrderByDescending(c => c.UnitsBuilt).Take(5);

            LinqHelperMethods.WriteResult(carriersAndCommonAircrafts, "Carriers and 5 common aircrafts ");
            */

            // zjistete kolik procent lidi prezilo v ramci evidovanych dat, v dotazu pouzijte klauzuli Aggregate(...)
            var aggregatedAirCrash = DataContext.AirCrashes.
                //Where(c => c.Fatalities != -1).
                Aggregate((a, b) => a.Aboard - b.Fatalities);
            
            var survivalPercentage = TODO: your code goes here

            //LinqHelperMethods.WriteResult(survivalPercentage, "Survival percentage is ");



            // naleznete letecke nehody pro 5 nejcastejsich
            // typu letadel (dle poctu vyrobenych kusu)
            // vysledek ulozte do lookupu kde:
            //   klic je typ letadla
            //   hodnotou je list leteckych nehod
            //var airCrashesAccordingTo5CommonAircraftTypes = TODO: your query goes here

            //LinqHelperMethods.WriteResult(airCrashesAccordingTo5CommonAircraftTypes, "Air crashes according To 5 common aircraft types");
        }

        public static void Task05()
        {
            // pro kazdy typ letadla (v leteckych nehodach) spocitejte
            // celkovy pocet obeti, vysledek ulozte do vhodne datove 
            // struktury, seradte dle poctu obeti a vypiste prvnich
            // 5 typu letadel a celkovy pocet obeti, nezapomente pak 
            // vyloucit pripady, kdy u dane letecke nehody neni znam 
            // typ letadla
            //var worst5 = TODO: your query goes here

            //LinqHelperMethods.WriteResult(worst5, "Top 5 number of fatalities for aircraft types");



            // z worst5 vyberte BEZ pouziti Where() ty zaznamy, ktere maji mene nez 3000 obeti
            //var specificRange = TODO: your query goes here

            //LinqHelperMethods.WriteResult(specificRange, "Fatalities (less than 3000) according to plane type");



            // z worst5 vyberte kazdy druhy typ letadla (zacnete prvnim prvkem)
            //var everySecond = TODO: your query goes here

            //LinqHelperMethods.WriteResult(everySecond, "Every second record according to plane type");



            // z worst5 vyberte zaznam, na nemz se pocet obeti nejvice blizi cislu 4000
            //var closestTo4000 = TODO: your query goes here

            //LinqHelperMethods.WriteResult(closestTo4000, "Fatalities according to plane type with fatalities closest to 4000: ");



            // z worst5 odeberte vsechny zaznamy letadel typu Douglas
            //var worst5WithoutDouglas = TODO: your query goes here

            //LinqHelperMethods.WriteResult(worst5WithoutDouglas, "Worst 5 fatalities according to plane type without all Douglas planes");
        }

        public static void Task06()
        {
            // naleznete rok, ve kterem se stalo nejvice leteckych nehod
            //var yearWithMostAirCrashes = TODO: your query goes here

            //LinqHelperMethods.WriteResult(yearWithMostAirCrashes, "Year with most air crashes: ");



            //pro nalezeny rok zjistete, kolik stroju od vyrobce Douglas melo v jen za tento rok nehodu
            //var douglasAirCrashes = TODO: your query goes here

            //LinqHelperMethods.WriteResult(douglasAirCrashes, $"Douglas air crashes in {yearWithMostAirCrashes}:");



            // naleznete nejtragictejsi rok (vzhledem k poctu obeti)
            //var mostTragicYear = TODO: your query goes here

            //LinqHelperMethods.WriteResult(mostTragicYear, "Most tragic year is: ");



            // vypiste plne nazvy leteckych spolecnosti, ktere 
            // pouzivaji letadla od spolecnosti Boeing a Airbus
            // (respektive se u nich eviduje havarie stroje u obou
            // vyse uvedenych vyrobcu)
            // Tip: Dotaz lze vhodne rozdelit na vice dilcich dotazu
            //var carriersUsingAirbusAndBoeingAircrafts = TODO: your query goes here


            //LinqHelperMethods.WriteResult(carriersUsingAirbusAndBoeingAircrafts, "Carriers using Airbus and Boeing aircrafts");
        }
    }
}
