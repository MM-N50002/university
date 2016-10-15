using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOPTest.Geometry;
using Geometry;

namespace OOPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("First rectangle: ");
            Rectangle<int> a = Rectangle<int>.GetFromConsole();
            Console.WriteLine("\nSecond rectangle: ");
            Rectangle<int> b = Rectangle<int>.GetFromConsole();
            
            var container = Rectangle<int>.GetContainer(new Rectangle<int>[] { a, b });
            var intersect = Rectangle<int>.GetIntersectOfTwo(a, b);

            Console.WriteLine("\nContainer:\n  " + container);

            if (intersect != null)
                Console.WriteLine("\nIntersection: \n  " + intersect);
            else
                Console.WriteLine("\nNo intersection of these two rectangles.");

            Console.ReadKey();
        }


    }
}
