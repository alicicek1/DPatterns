using static System.Console;
using System;
using System.Collections.Generic;

namespace BuilderDirectorSample
{
    public class Product
    {
        private List<string> c_parts = new List<string>();

        public void Add(string part)
        {
            c_parts.Add(part);
        }

        public override string ToString()
        {
            string s = "Components : \n\r";

            foreach (string item in c_parts)
            {
                s += item + "\n\r";
            }
            return s;
        }
    }
    public abstract class Builder
    {
        public abstract void BuildPart(string component);
        public abstract Product GetProduct();
    }

    class ConcreteBuilder1 : Builder
    {
        private Product product = new Product();
        public override void BuildPart(string component)
        {
            product.Add(component);
        }

        public override Product GetProduct()
        {
            return product;
        }
    }


    class ConcreteBuilder2 : Builder
    {
        private Product product = new Product();
        public override void BuildPart(string component)
        {
            product.Add(component);
        }

        public override Product GetProduct()
        {
            return product;
        }
    }

    public class Director
    {
        public static void Construct(Builder builder, string[] components)
        {
            foreach (string item in components)
            {
                builder.BuildPart(item);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] compList = { "a", "b", "c" };
            string[] compList2 = { "a", "b", "c" };

            Builder b = new ConcreteBuilder1();
            Director.Construct(b, compList);
            Product product = b.GetProduct();
            WriteLine(product.ToString());

            WriteLine("************************");

            b = new ConcreteBuilder2();
            Director.Construct(b, compList2);
            Product product1 = b.GetProduct();
            WriteLine(product1.ToString());
        }
    }
}
