using System;
using System.Collections.Generic;
using System.Linq;

namespace FuntionalBuilder
{
    public class Person
    {
        public string Name, Position;
    }

    public sealed class PersonBuilder  // => sealed class means that you cannot inherit from it or if you you want to extend some you don't have ability to use inheritance
    {
        private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

        public PersonBuilder Do(Action<Person> action) => AddAction(action);

        public PersonBuilder Called(string name) => Do(p => p.Name = name);

        public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));

        private PersonBuilder AddAction(Action<Person> action)
        {
            actions.Add(p => { action(p); return p; });
            return this;
        }
    }


    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

        public TSelf Do(Action<Person> action) => AddAction(action);

        public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));

        private TSelf AddAction(Action<Person> action)
        {
            actions.Add(p => { action(p); return p; });
            return (TSelf)this;
        }
    }

    public sealed class PersonBuilderFuntional : FunctionalBuilder<Person, PersonBuilderFuntional>
    {
        public PersonBuilderFuntional Called(string name) => Do(p => p.Name = name);

    }

    public static class PersonBuilderExtension
    {
        public static PersonBuilder PersonBuilderWorkAs(this PersonBuilder builder, string position) => builder.Do(p => p.Position = position);
    }

    public static class PersonBuilderExtensionFunctionl
    {
        public static PersonBuilderFuntional PersonBuilderWorkAs(this PersonBuilderFuntional builder, string position) => builder.Do(a => a.Position = position);
    }

    class Program
    {
        static void Main(string[] args)
        {
            var person = new PersonBuilder()
                .PersonBuilderWorkAs("Developer")
                .Called("Sara")
                .Build();
        }
    }
}
