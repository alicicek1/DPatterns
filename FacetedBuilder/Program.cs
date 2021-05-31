
using System;

namespace FacetedBuilder
{
    public class Person
    {
        public string streetAddress, postCode, city;
        public string companyName, posititon;
        public int income;

        public override string ToString()
        {
            return $"{nameof(streetAddress)}: {streetAddress}, {nameof(postCode)}: {postCode}, {nameof(city)}: {city}, {nameof(companyName)}: {companyName}, {nameof(posititon)}: {posititon}, {nameof(income)}: {income}";
        }
    }

    /*
     facade => it does not actually build up person by itself, but it keeps a reference to the person that's being built up.
    and it allows you access to those sub builders.
    */
    public class PersonBuilder
    {
        protected Person person = new Person(); //reference object
        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string compName)
        {
            person.companyName = compName;
            return this;
        }

        public PersonJobBuilder AsA(string pos)
        {
            person.posititon = pos;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.income = amount;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder Street(string street)
        {
            person.streetAddress = street;
            return this;
        }

        public PersonAddressBuilder City(string city)
        {
            person.city = city;
            return this;
        }

        public PersonAddressBuilder PostalCode(string pC)
        {
            person.postCode = pC;
            return this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb.Works.At("MIT")
                                    .AsA("Developer")
                                    .Earning(9000);
            pb.Lives.Street("Manhattan")
                    .PostalCode("123")
                    .City("NY");
            Console.WriteLine("Hello World!");
        }
    }
}
