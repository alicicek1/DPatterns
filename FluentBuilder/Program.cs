using System;
using static System.Console;

namespace FluentBuilder
{

    public class Person
    {
        public string Name;
        public string Position;
        public DateTime DateOfBirth;

        public class Builder : PersonBirthDateBuilder<Builder>
        {
            internal Builder() { }
        }

        public static Builder New => new Builder();
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}, {nameof(DateOfBirth)}: {DateOfBirth}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build() { return person; }
    }

    public class PersonInfoBuilder<T>
        : PersonBuilder
        where T : PersonInfoBuilder<T>
    {
        public T Called(string name)
        {
            person.Name = name;
            return (T)this;
        }
    }

    public class PersonJobBuilder<T>
        : PersonInfoBuilder<PersonJobBuilder<T>>
        where T : PersonJobBuilder<T>
    {
        public T WorkAsA(string position)
        {
            person.Position = position;
            return (T)this;
        }
    }

    public class PersonBirthDateBuilder<T>
        : PersonJobBuilder<PersonBirthDateBuilder<T>>
        where T : PersonBirthDateBuilder<T>
    {
        public T Born(DateTime date)
        {
            person.DateOfBirth = date;
            return (T)this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var me = Person.New
                .Called("Ali")
                .WorkAsA("Backend Developer")
                .Born(DateTime.UtcNow)
                .Build();

            WriteLine(me);
        }
    }
}
