using System;

namespace Lab._10
{
    internal class ClassPoint
    {
        public class PointArray
        {
            private Point[] _coordinateArray;

            //Конструктор без атрибутов
            public PointArray() {
                _coordinateArray = null;
                GetSize = 0;
            }

            //Заполнение рандомом
            public PointArray(int size, int min, int max) {
                GetSize = size;
                _coordinateArray = new Point[GetSize];
                var rnd = new Random();

                for (var i = 0; i < GetSize; i++)
                    _coordinateArray[i] = new Point(rnd.Next(min, max + 1) + rnd.NextDouble(),
                        rnd.Next(min, max + 1) + rnd.NextDouble());
            }

            //Создание массива с клавиатуры
            public PointArray(int size) {
                GetSize = size;
                _coordinateArray = new Point[GetSize];

                for (var i = 0; i < GetSize; i++) _coordinateArray[i] = MainCode.ReadCoordinate(i + 1);
            }

            //Полчение size
            public int GetSize { get; private set; }

            //Индексатор
            public Point this[int index] {
                get {
                    if (index >= 0 && index < GetSize) return _coordinateArray[index];

                    Console.WriteLine("Ошибка! Индекс задан неправильно.");
                    return new Point(-1000, 1);
                }
                set {
                    if (index >= 0 && index < GetSize)
                        _coordinateArray[index] = value;
                    else
                        Console.WriteLine("Ошибка! Индекс задан неправильно.");
                }
            }

            //Показать массив координат
            public void Show() {
                foreach (var element in _coordinateArray) {
                    element.Show();
                    Console.WriteLine("");
                }
            }


            //Перегрузка метода Resize
            public void Resize(int lenght) {
                GetSize = lenght;
                Array.Resize(ref _coordinateArray, lenght);
            }
        }

        public class Point
        {
            //Конструктор класса. Инициализация Координаты (x,y).
            public Point(double x, double y) {
                Count++;
                CoordinateX = x;
                CoordinateY = y;
            }

            //Считываем значение cxount
            public static int Count { get; private set; }

            //Получение значения x
            public double CoordinateX { get; private set; }

            //Получение значение y
            public double CoordinateY { get; }

            //Показать координаты точки.
            public void Show() {
                Console.Write("({0:0.###};{1:0.###})", CoordinateX, CoordinateY);
            }

            //Высчитываем дистанцию между 2-мя точками
            public double Distance(Point coordinate2) {
                double distance;
                var vector = new Point(coordinate2.CoordinateX - CoordinateX, coordinate2.CoordinateY - CoordinateY);
                distance = vector.LengthVector();
                return distance;
            }

            //Высчитываем длину вектора
            private double LengthVector() {
                return Math.Sqrt(Math.Pow(CoordinateX, 2) + Math.Pow(CoordinateY, 2));
            }

            //Перегрузка унарной операции ++ 
            public static Point operator ++(Point coordinate) {
                coordinate.CoordinateX += 1;
                return coordinate;
            }

            //Перегрузка унарной операции --
            public static Point operator --(Point coordinate) {
                coordinate.CoordinateX -= 1;
                return coordinate;
            }

            //Перегрузка операции приведения типа: явная?
            public static explicit operator int(Point coordinate) {
                return (int) coordinate.CoordinateX;
            }

            //Перегрузка операции приведения типа: неявная
            public static implicit operator double(Point coordinate) {
                return coordinate.CoordinateY * 0.1 / 0.1;
            }

            //Перегрузка операции + Point p
            public static double operator +(Point coordinate1, Point coordinate2) {
                return coordinate1.Distance(coordinate2);
            }

            //Перегрузка операции + int (правостороннее)
            public static Point operator +(Point coordinate, int x) {
                coordinate.CoordinateX += x;
                return coordinate;
            }

            //Перегрузка операции + int (лeвостороннее)
            public static Point operator +(int x, Point coordinate) {
                coordinate.CoordinateX += x;
                return coordinate;
            }

            //Перегрузка операции >
            public static bool operator >(Point element, Point y) {
                var distance = element.Distance(y);
                if (distance > y)
                    return true;
                return false;
            }

            //Перегрузка операции <
            public static bool operator <(Point element, Point y) {
                var distance = element.Distance(y);
                if (distance > y)
                    return true;
                return false;
            }

            //Перегрузка операции ==
            public static bool operator ==(Point element, Point y) {
                var distance = element.Distance(y);
                if (distance == y)
                    return true;
                return false;
            }

            //Перегрузка операции !=
            public static bool operator !=(Point element, Point y) {
                var distance = element.Distance(y);
                if (distance > y)
                    return true;
                return false;
            }
        }
    }
}