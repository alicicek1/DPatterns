using System;
using System.Collections.Generic;
using static System.Console;

namespace OpenClosedPrinciple
{
    public enum Color
    {
        Green, Blue, Red
    }

    public enum Size
    {
        Small, Medium, Large, Yuge
    }

    public class Product
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }

            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySİze(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
                if (p.Size == size)
                    yield return p;
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;
        public ColorSpecification(Color color)
        {
            this.color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }

    public class SizeSpesification : ISpecification<Product>
    {
        private Size size;

        public SizeSpesification(Size size)
        {
            this.size = size;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var p in items)
                if (spec.IsSatisfied(p))
                    yield return p;

        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first == null ? throw new ArgumentNullException(paramName: nameof(first)) : first;
            this.second = second == null ? throw new ArgumentNullException(paramName: nameof(second)) : second;
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Yuge);

            Product[] products = { apple, tree, house };
            var pf = new ProductFilter();
            WriteLine("Small product (old):");
            foreach (var item in pf.FilterBySİze(products, Size.Small))
            {
                WriteLine($" - {item.Name} is Small");
            }

            var bf = new BetterFilter();
            WriteLine("Green product (old):");
            foreach (var item in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                WriteLine($" - {item.Name} is Green");
            }

            WriteLine("Large blue items");
            foreach (var item in bf.Filter(products, new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpesification(Size.Yuge))))
            {
                WriteLine($" - {item.Name} is big blue and yuge.");
            }
        }
    }
}
