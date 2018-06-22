using System.Linq;

namespace Tuples
{
    internal class Person
    {
        public int Age { get; }
        public string Name { get; }
        public bool LikesIceHockey { get; }

        public Person(int age, bool likesHockey, string name) =>
            (Age, LikesIceHockey, Name) = (age, likesHockey, name);

        public void Deconstruct(out int age, out string name)
        {
            age = Age;
            name = Name;
        }

        public void Deconstruct(out bool likesHockey, out int age, out string name)
        {
            likesHockey = LikesIceHockey;
            age = Age;
            name = Name;
        }

        public (string firstName, string lastName) GetFullName()
        {
            var parts = Name.Split(' ');
            return (parts.FirstOrDefault(), parts.LastOrDefault());
        }
    }

}
