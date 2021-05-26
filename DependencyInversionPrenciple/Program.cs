using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace DependencyInversionPrenciple
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
    }

    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

        public void AddParentAncChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllCildrenOf(string name)
        {
            foreach (var r in relations.Where(
                a => a.Item1.Name == name &&
                a.Item2 == Relationship.Parent
                ))
            {
                yield return r.Item3;
            }
        }

        public List<(Person, Relationship, Person)> Relations => relations;
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllCildrenOf(string name);
    }

    class Program
    {
        //public Program(Relationships relationships)
        //{
        //    var relations = relationships.Relations;
        //    foreach (var r in relations.Where(
        //        a => a.Item1.Name == "Nick" &&
        //        a.Item2 == Relationship.Parent
        //        ))
        //    {
        //        WriteLine($"Nick has a child called {r.Item3.Name}");
        //    }
        //}

        public Program(IRelationshipBrowser browser)
        {
            foreach (var item in browser.FindAllCildrenOf("Nick"))
            {
                WriteLine($"Nick has a child called {item.Name}");
            }
        }
        static void Main(string[] args)
        {
            var parent = new Person() { Name = "Nick" };
            var child1 = new Person() { Name = "Marry" };
            var child2 = new Person() { Name = "Lea" };

            var relationships = new Relationships();
            relationships.AddParentAncChild(parent, child1);
            relationships.AddParentAncChild(parent, child2);

            new Program(relationships);
        }
    }
}
