using System;

namespace LinqToXml.SampleData
{
    public static class Paths
    {
        private static readonly string SampleDataFolder = AppDomain.CurrentDomain.BaseDirectory + @"..\..\SampleData\";

        public static readonly string CustomersAndOrders = SampleDataFolder + "CustomersAndOrders.xml";

        public static readonly string MovieRatings = SampleDataFolder + "MovieRatings.xml";

        public static readonly string Movies = SampleDataFolder + "Movies.xml";

        public static readonly string UpdatedMovies = SampleDataFolder + "UpdatedMovies.xml";
    }
}
