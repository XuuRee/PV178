using System;

namespace cv04
{
    /// <summary>
    /// All credit for demo samples goes to Lukas Daubner
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Otestovani samostatne prace na cviceni (zadani viz Lab04_Tasks)
            // Solution.TestSolution();

            var point = new FancyPoint();
            point.ValueChanged += ValueChangedMethod;

            point.X = 50;
            point.Y = 35;
            point.X = point.Y;

            // Co zde chybi ?

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void ValueChangedMethod(object sender, EventArgs e)
        {
            Console.WriteLine($"An {sender.GetType()} object just fired an event!");
            var fancyE = (FancyEventArgs)e;
            Console.WriteLine("property: {2}, previously: {0}, now: {1}", fancyE.PrewValue, fancyE.NewValue, fancyE.PropertyName);
        }
    }
}
