using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPTest.Geometry
{
    //Basic content
    public partial class Rectangle<T> : Element where T: struct
    {
        Comparer<T> Comp { get; set; }
        /// <summary>
        /// Две точки для построения прямоугольника. 
        /// </summary>
        private Point<T>[] Points { get; set; }

        /// <summary>
        /// Конструктор по двум точкам
        /// </summary>
        /// <param name="pointA">Первая точка</param>
        /// <param name="pointB">Вторая точка</param>
        public Rectangle(Point<T> pointA, Point<T> pointB)
        { 
            Points = new Point<T>[2];
            Points[0] = pointA;
            Points[1] = pointB;
            Comp = Comparer<T>.Default;
        }
        /// <summary>
        /// Конструктор по четырем координатам точек
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public Rectangle(T x1, T y1, T x2, T y2)
        {
            Points = new Point<T>[2];
            Points[0] = new Point<T> { X = x1, Y = y1 };
            Points[1] = new Point<T> { X = x2, Y = y2 };
            Comp = Comparer<T>.Default;
        }
        /// <summary>
        /// Передвинуть прямоугольник
        /// </summary>
        /// <param name="x">Горизонтально</param>
        /// <param name="y">Вертикально</param>
        public void Move(T x, T y)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i].X += x;
                Points[i].Y += y;
            }
        }
        /// <summary>
        /// Изменение размера прямоугольника
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        public void Resize(T width, T height)
        {
            Points[1] = new Point<T>
            {
                X = Points[0].X + width,
                Y = Points[0].Y + height
            };
        }
        /// <summary>
        /// Получить минимальные координаты
        /// </summary>
        /// <returns></returns>
        private Point<T> GetMinValues()
        {
            
            T minX = Points[0].X;
            T minY = Points[0].Y;
            for (int i = 0; i < Points.Length; i++)
            {

                minX = GetMinimal(minX, Points[i].X);
                minY = GetMinimal(minY, Points[i].Y);

                //minX = Math.Min(minX, Points[i].X);
                //minY = Math.Min(minY, Points[i].Y);
            }

            return new Point<T> { X = minX, Y = minY };
        }
        /// <summary>
        /// Получить максимальные координаты
        /// </summary>
        /// <returns></returns>
        private Point<T> GetMaxValues()
        {
            T maxX = Points[0].X;
            T maxY = Points[0].Y;
            
            for (int i = 0; i < Points.Length; i++)
            {
                maxX = GetMaximal(maxX, Points[i].X);
                maxY = GetMaximal(maxY, Points[i].Y);
                
            }

            return new Point<T> { X = maxX, Y = maxY };
        }
        /// <summary>
        /// Получить минимальный из аргументов типа T
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private T GetMinimal(T x, T y)
        {
            if (Comp.Compare(x, y) == 1)
            {
                return y;
            }
            return x;
        }
        /// <summary>
        /// Получить максимальный из аргументов типа T
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private T GetMaximal(T x, T y)
        {
            if (Comp.Compare(x, y) == -1)
            {
                return y;
            }
            return x;
        }
        /// <summary>
        /// Получить массив всех точек прямоугольника
        /// </summary>
        /// <returns></returns>
        private Point<T>[] GetAllPoints()
        {
            var p1 = Points[0];
            var p2 = new Point<T> { X = Points[0].X, Y = Points[1].Y };
            var p3 = new Point<T> { X = Points[1].X, Y = Points[0].Y };
            var p4 = Points[1];
            return new Point<T>[] { p1, p2, p3, p4 };

        }
        /// <summary>
        /// Получить прямоугольник, содержащий прямоугольники
        /// </summary>
        /// <param name="rectangles">Массив прямоугольников</param>
        /// <returns></returns>
        public static Rectangle<T> GetContainer(Rectangle<T>[] rectangles)
        {
            Point<T>[] _points = new Point<T>[2]; 

            for (int i = 0; i < _points.Length; i++) //инициализирую оба вектора нулевыми значениями из первой точки
            {
                _points[i] = new Point<T>
                {
                    X = rectangles[0].Points[0].X,
                    Y = rectangles[0].Points[0].Y
                };
            }

            foreach (var rect in rectangles)
            {
                Point<T> min = rect.GetMinValues();
                Point<T> max = rect.GetMaxValues();

                _points[0].X = Math.Min(_points[0].X, min.X);
                _points[0].Y = Math.Min(_points[0].Y, min.Y);

                _points[1].X = Math.Max(_points[1].X, max.X);
                _points[1].Y = Math.Max(_points[1].Y, max.Y);
            }

            return new Rectangle<T>(_points[0], _points[1]);
        }
        /// <summary>
        /// Получить пересечение двух прямоугольников
        /// </summary>
        /// <param name="a">Первый прямоугольник</param>
        /// <param name="b">Второй прямоугольник</param>
        /// <returns></returns>
        public static Element GetIntersectOfTwo(Rectangle<T> a, Rectangle<T> b)
        {
            var minA = a.GetMinValues();
            var maxA = a.GetMaxValues();

            var minB = b.GetMinValues();
            var maxB = b.GetMaxValues();

            bool intersection = ((maxB.X >= minA.X && minB.X <= maxA.X) && (maxB.Y >= minA.Y && minB.Y <= maxA.Y));

            if (!intersection)
            { 
                return null;
            }

            var x1 = Math.Max(minA.X, minB.X);
            var y1 = Math.Min(maxA.Y, maxB.Y);
            var x2 = Math.Min(maxA.X, maxB.X);
            var y2 = Math.Max(minA.Y, minB.Y);

            Point<T> p1 = new Point<T> { X = x1, Y = y1 };
            Point<T> p2 = new Point<T> { X = x2, Y = y2 };

            if (p1 == p2) //проверка, если пересечение - точка
                return p1;

            return new Rectangle<T>(x1, y1, x2, y2);
        }
        /// <summary>
        /// Считать прямоугольник из консоли
        /// </summary>
        /// <returns></returns>
        public static Rectangle<T> GetFromConsole()
        {
            Console.WriteLine("Input X1 Y1 X2 Y2:");

            var coords = Console.ReadLine().Split(' ');
            var rectArr = ParseCoords(coords);

            return new Rectangle<T>(rectArr[0], rectArr[1], rectArr[2], rectArr[3]);
        }
        /// <summary>
        /// Парсинг координат из строки в массив
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="coords"></param>
        private static T[] ParseCoords(string[] coords)
        {
            try
            {
                T[] arr = new T[coords.Length];
                for (int i = 0; i < coords.Length; i++)
                {
                    arr[i] = (T)Convert.ChangeType(float.Parse(coords[i]), typeof(T));
                }
                return arr;
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Exception: The input string has wrong format.");
                throw;
            }
        }

    }
    //Overrides
    public partial class Rectangle<T> where T: struct
    {
        public override string ToString()
        {
            return string.Format(
                "Rectangle: X1[{0}] Y1[{1}] X2[{2}] Y2[{3}]",
                Points[0].X, Points[0].Y,
                Points[1].X, Points[1].Y
                );
        }
        public override string GetDescription()
        {
            return "Rectangle. 4 points, 4 lines, 1 square.";
        }
        public static bool operator ==(Rectangle<T> obj1, Rectangle<T> obj2)
        {
            if ((obj1.GetMinValues() == obj2.GetMinValues()) && (obj1.GetMaxValues() == obj2.GetMaxValues()))
                return true;
            return false;
        }
        public static bool operator !=(Rectangle<T> obj1, Rectangle<T> obj2)
        {
            if ((obj1.GetMinValues() != obj2.GetMinValues()) || (obj1.GetMaxValues() != obj2.GetMaxValues()))
                return true;
            return false;
        }
    }
}
