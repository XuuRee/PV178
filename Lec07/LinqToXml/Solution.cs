using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using LinqToXml.SampleData;

namespace LinqToXml
{
    public static class Solution
    {
        public static void Tasks()
        {
            var moviesRoot = XElement.Load(Paths.Movies);
            var movieRatingssRoot = XElement.Load(Paths.MovieRatings);

             /* TODO 
              Za pomocí Linq to Xml zjistěte všechny žánry filmů, které umístěte do jednoho řetězce a oddělte čárkami
             */
            var allGenres = moviesRoot
                .Elements("Movie")
                .Descendants("Genre")
                .Select(element => element.Value)
                .Distinct()
                .Aggregate((first, second) => $"{first}, {second}");
            Console.WriteLine($"All genres: {allGenres}" + Environment.NewLine);
            
            /* TODO 
              Za pomocí Linq to Xml spočítejte průměrné hodnocení všech filmů
             */
            var averageRating = movieRatingssRoot
                .Elements("Movie")
                .Descendants("Score")
                .Select(scoreElement =>
                {
                    var rating = double.Parse(scoreElement.Value.Replace(',', '.'), CultureInfo.InvariantCulture);
                    if (Math.Abs(rating % 1) < 0.00000001)
                    {
                        return rating / 100.0;
                    }
                    return rating / 10.0;
                })
                .Average();
            Console.WriteLine($"Average movie rating: {averageRating:N2}" + Environment.NewLine);

            /* TODO 
              Za pomocí Linq to Xml získejte z Movies.xml a MovieRatings.xml následující údaje:
                název filmu, 
                jméno režiséra (spojte jméno a příjmení), 
                veškerá hodnocení (stačí ve formě List<string>, do výsledku zahrnujte pouze filmy, které mají alespoň jedno hodnocení) 
             */
            var moviesWithRatings = moviesRoot.Elements("Movie")
                .Join(movieRatingssRoot.Elements("Movie"), movieElement => movieElement.FirstAttribute.Value,
                    movieRatingElement => movieRatingElement.FirstAttribute.Value,
                    (movieElement, movieRatingElement) =>
                        new
                        {
                            MovieName = movieElement.FirstAttribute.Value,
                            Director = movieElement.Element("Director")?.Descendants()
                                    .Select(directorElement => directorElement.Value)
                                    .Aggregate((first, second) =>  $"{first} {second}"),
                            Ratings = movieRatingElement.Descendants("Score").Select(scoreElement => scoreElement.Value).ToList()
                        })
                .ToList();

            /* TODO 
              I.    Do Movies.xml přidejte xml element "Movie" s filmem "Pulp Fiction"*, jako režiséra uveďte Quentina Tarantina, 
                    žánr nastavte na Thriller, jako rok vydání uveďte 1994 a vytvořte prozatím prázdný element "Cast"
                    * název filmu uveďte jako hodnotu atributu "MovieName" elementu "Movie"

              II.   Dovnitř elementu "Cast" vložte všechny herce z ostatních filmů (zachovejte strukturu elementu "Actor")

              III.  Při kontrole správnosti údajů se zjistilo, že kromě herce Samuela L. Jacksona v Pulp Fiction žádný z 
                    uvedených herců nehraje, smažte tedy všechny herce, kteří ve filmu neučinkovali (použijte LinqToXml).

              IV.   Element s filmem "Pulp Fiction" vypište do konzoly.

             */

            // I.
            moviesRoot.Add(
                new XElement("Movie", 
                    new XElement("Director", new XElement("FistName", "Quentin"), new XElement("LastName", "Tarantino")), 
                    new XElement("Genres", new XElement("Genre", "Thriller")), 
                    new XElement("ReleaseYear", 1994),
                    new XElement("Cast")));
            var pulpFiction = moviesRoot.Elements().Last();
            pulpFiction.SetAttributeValue("MovieName", "Pulp Fiction");

            // II.
            var actors = moviesRoot.Descendants("Actor").ToList();
            pulpFiction.Element("Cast")?.Add(actors);

            // III.
            pulpFiction.Element("Cast")?.Elements()
                .Where(element =>
                {
                    var firstName = element.Element("FirstName");
                    var lastName = element.Element("LastName");
                    return lastName != null && firstName != null && 
                       (!firstName.Value.Equals("Samuel") || !lastName.Value.Equals("Jackson"));
                })
                .Remove();
            Console.WriteLine(Environment.NewLine + "Pulp Fiction movie element:");

            // IV.
            Console.WriteLine(pulpFiction);

            // Save the updated xml
            using (TextWriter writer = File.CreateText(Paths.UpdatedMovies))
            {
                moviesRoot.Save(writer);
            }
        }
    }
}
