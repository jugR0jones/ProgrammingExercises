// See https://aka.ms/new-console-template for more information

internal static class Program
{
    internal class Person
    {
        public string Name { get; set; }
        public string Surname;

        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
    
    internal class ClassWithProperties
    {
        public Person ExpressionBodiedMember => new Person("A", "AA");

        public Person Person1
        {
            get
            {
                return new Person("B", "BB");
            }
        }

        public Person Person2 { get; } = new Person("C", "CC");

        private Person person3 = new Person("D", "DD");
        public Person Person3
        {
            get
            {
                return person3;
            }
        }

        public Person Person4 => person3;
    }
    
    internal static void Main()
    {
        ClassWithProperties classWithProperties = new ClassWithProperties();
        
        Console.WriteLine(classWithProperties.ExpressionBodiedMember.GetHashCode());
        Console.WriteLine(classWithProperties.ExpressionBodiedMember.GetHashCode());
        Console.WriteLine(classWithProperties.ExpressionBodiedMember.GetHashCode());
        Console.WriteLine("-----------------------");
        Console.WriteLine(classWithProperties.Person1.GetHashCode());
        Console.WriteLine(classWithProperties.Person1.GetHashCode());
        Console.WriteLine(classWithProperties.Person1.GetHashCode());
        Console.WriteLine("-----------------------");
        Console.WriteLine(classWithProperties.Person2.GetHashCode());
        Console.WriteLine(classWithProperties.Person2.GetHashCode());
        Console.WriteLine(classWithProperties.Person2.GetHashCode());
Console.WriteLine("-----------------------");
        Console.WriteLine(classWithProperties.Person3.GetHashCode());
        Console.WriteLine(classWithProperties.Person3.GetHashCode());
        Console.WriteLine(classWithProperties.Person3.GetHashCode());
        Console.WriteLine("-----------------------");
        Console.WriteLine(classWithProperties.Person4.GetHashCode());
        Console.WriteLine(classWithProperties.Person4.GetHashCode());
        Console.WriteLine(classWithProperties.Person4.GetHashCode());
    }
}