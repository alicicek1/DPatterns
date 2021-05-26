using System;
using static System.Console;

namespace InterfaceSegregationPrinciple
{
    public class Document
    {
    }

    public interface IMachine
    {
        void Print(Document document);
        void Scan(Document document);
        void Fax(Document document);
    }

    public class MultiFuncPrinter : IMachine
    {
        public void Fax(Document document)
        {
            //
        }

        public void Print(Document document)
        {
            //
        }

        public void Scan(Document document)
        {
            //
        }
    }

    public class OldFashionPrinter : IMachine
    {
        public OldFashionPrinter()
        {
        }

        public void Fax(Document document)
        {
            //
        }

        public void Print(Document document)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPrinter
    {
        void Print(Document document);
    }

    public interface IScanner
    {
        void Scan(Document document);
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document document)
        {
            //
        }

        public void Scan(Document document)
        {
            //
        }
    }

    public interface IMultiFuncDevice : IScanner, IPrinter
    {
    }

    public class MultiFuncMachine : IMultiFuncDevice
    {
        private IPrinter printer;
        private IScanner scanner;
        public MultiFuncMachine(IPrinter printer, IScanner scanner)
        {
            if (printer == null)
            {
                throw new ArgumentNullException(paramName: nameof(printer));
            }

            if (scanner == null)
            {
                throw new ArgumentNullException(paramName: nameof(scanner));
            }

            this.printer = printer;
            this.scanner = scanner;
        }

        //DECORATor
        public void Print(Document document)
        {
            printer.Print(document);
        }

        public void Scan(Document document)
        {
            scanner.Scan(document);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
