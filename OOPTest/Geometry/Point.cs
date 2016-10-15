using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPTest.Geometry
{
    public class Point<T> : Element
    {
        public dynamic X { get; set; }
        public dynamic Y { get; set; }
        public override string ToString()
        {
            return string.Format("Point: X[{0}] Y[{1}]", X, Y);
        }
        public static bool operator ==(Point<T> obj1, Point<T> obj2)
        {

            if ((obj1.X.Equals(obj2.X)) && (obj1.Y.Equals(obj2.Y)))
                return true;
            return false;
        }
        public static bool operator !=(Point<T> obj1, Point<T> obj2)
        {
            if (!(obj1.X.Equals(obj2.X)) || (obj1.Y.Equals(obj2.Y)))
                return true;
            return false;
        }
        public override string GetDescription()
        {
            return "If it walks like a point and quacks like a point, it must be a point.";
        }
        

        
    } 
}
