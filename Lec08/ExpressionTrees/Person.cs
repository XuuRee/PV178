namespace ExpressionTrees
{
    public class Person
    {
        public string Name { get; }
        public int Age { get; }
        public bool IsSingle { get; }

        public Person(string name, int age, bool isSingle)
        {
            Name = name;
            Age = age;
            IsSingle = isSingle;
        }
    }
}
