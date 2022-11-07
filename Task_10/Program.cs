using System;
using System.Collections.Generic;
using System.Linq;


namespace Task_10
{
    /// <summary>
    /// Задача 10. "Деление многоугольного торта пополам" (нахождение средней точки методом последовательных приближений)
    /// </summary>
    public static class Program
    {
        public const double DESIRED_ACCURACY = 0.000_001;

        static void Main()
        {
            Console.WriteLine("Введите кол-во вершин выпуклого многоугольника:");
            uint edgeCount = uint.Parse(Console.ReadLine().Trim());
            if (edgeCount < 1 || edgeCount > 1000)
            {
                throw new ArgumentOutOfRangeException("Количество вершин многоугольника должно быть (1 ≤ n ≤ 1000)!");
            }            
            Console.WriteLine("Введите координаты вершин многоугольника (целые числа) - каждую пару с новой строки:");
            var points = new List<KeyValuePair<int, int>>((int)edgeCount);
            int i = 0;
            do
            {
                var arrXY = Console.ReadLine().Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                points.Add(new KeyValuePair<int, int>(ParseCoord(arrXY[0]), ParseCoord(arrXY[1])));
                i++;
            }
            while (i < edgeCount);
            if (edgeCount <= 2)
            {
                Console.WriteLine(0);
            }
            else
            {
                double square = CalculatePolygonSquare(points.Select(x => new KeyValuePair<double, double>(x.Key, x.Value)).ToList());
                double res = GetXCoordOfHalfLine(points, square / 2);
                Console.WriteLine("{0:f9}", Math.Round(res, 9));
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Метод поиска координаты Х, вертикальная линия проведённая через которую делит фигуру пополам (на две равные 
        ///     по площади части).
        /// </summary>
        /// <param name="points">Набор координат выпуклого многоугольника</param>
        /// <param name="desiredAccuracy">
        /// Желаемая точность (разность площадей левой и правой части), при достижении которой решение считается найденным.
        /// </param>
        /// <remarks>
        /// Площадь считается методом последовательных приближений по формуле "Шнуровки Гаусса" (<see cref="https://ru.wikipedia.org/wiki/%D0%A4%D0%BE%D1%80%D0%BC%D1%83%D0%BB%D0%B0_%D0%BF%D0%BB%D0%BE%D1%89%D0%B0%D0%B4%D0%B8_%D0%93%D0%B0%D1%83%D1%81%D1%81%D0%B0"/>)
        /// </remarks>
        /// <returns>
        /// Координата X вертикальной линии делящей фигуру пополам
        /// </returns>
        public static double GetXCoordOfHalfLine(IList<KeyValuePair<int, int>> points, double halfSquare, 
            double startX = double.NaN, double desiredAccuracy = DESIRED_ACCURACY)
        {
            if (halfSquare == 0)
            {
                throw new ArgumentException($"Параметр {nameof(halfSquare)} должен быть строго больше 0!", nameof(halfSquare));
            }
            double middleX = startX;
            double? middleY1 = null, middleY2 = null;
            // пробуем найти простое решение - ищем координату половины ширины фигуры по оси Х                        
            if (double.IsNaN(startX))
            {
                int max_X = points[0].Key;
                int min_X = points[0].Key;
                foreach (var p in points)
                {
                    if (p.Key > max_X)
                    {
                        max_X = p.Key;
                    }
                    else if (p.Key < min_X)
                    {
                        min_X = p.Key;
                    }
                }
                middleX = (max_X + min_X) / 2f;
            }            
            // проверяем предельный случай, когда вершины попадают в существующие.            
            var knownCrossPoints = points.Where(y => Math.Abs(middleX - y.Key) <= desiredAccuracy);
            using (var iter = knownCrossPoints.GetEnumerator())
            {
                if (iter.MoveNext())
                {
                    middleY1 = iter.Current.Value;
                }
                if (iter.MoveNext())
                {
                    middleY2 = iter.Current.Value;
                }
            }
            // ищем 2 (или 4) ближайшие вершины между которыми попала точка middleX (снизу и сверху)
            KeyValuePair<int, int>? leftPoint_1 = null, leftPoint_2 = null;
            KeyValuePair<int, int>? rightPoint_1 = null, rightPoint_2 = null;
            // если среди вершин сразу средний Y найти не удалось, то ищем верхнюю и нижнюю грани выпуклого многоугольника,
            //      по которым идёт пересечение линией проведённой через предполагаемый "средний" X.            
            // точку(и) пересечения будем считать по параметрическому(им) уравнению(ям) прямой(ых) (см. http://www.cleverstudents.ru/line_and_plane/line_passes_through_2_points.html)
            // если мы нашли ещё не все точки пересечения, то ищем оставшиеся
            for (int i = 0, nextPointIndex = 0; i < points.Count && (!middleY1.HasValue || !middleY2.HasValue); i++)
            {
                if (points[i].Key < middleX) 
                {
                    nextPointIndex = (i + 1 == points.Count) ? 0 : i + 1;
                    if (points[nextPointIndex].Key > middleX)
                    {
                        leftPoint_1 = points[i];
                        rightPoint_1 = points[nextPointIndex];
                        // если только один Y ранее совпал с одной из вершин, значит мы неправильно поняли, какой это Y по счёту.
                        if (middleY1.HasValue)
                        {
                            middleY2 = middleY1;
                        }
                        middleY1 = CalculateLinear_Y(middleX, leftPoint_1.Value, rightPoint_1.Value);
                    }
                }
                else if (points[i].Key > middleX)
                {
                    nextPointIndex = i + 1 != points.Count ? i + 1 : 0;
                    if (points[nextPointIndex].Key < middleX)
                    {
                        leftPoint_2 = points[nextPointIndex];
                        rightPoint_2 = points[i];
                        // если только один Y ранее совпал с одной из вершин, значит мы неправильно поняли, какой это Y по счёту.
                        if (middleY2.HasValue)
                        {
                            middleY1 = middleY2;
                        }
                        middleY2 = CalculateLinear_Y(middleX, leftPoint_2.Value, rightPoint_2.Value);
                    }                    
                }
            }
            // верхняя точка пересечения с линией деления
            var crossPoint_1 = new KeyValuePair<double, double>(middleX, middleY1.Value);
            // нижняя точка пересечения с линией деления
            var crossPoint_2 = new KeyValuePair<double, double>(middleX, middleY2.Value);
            // рассчитываем площади двух половин фигуры с учётом двух новых точек
            var leftHalfPoints = new List<KeyValuePair<double, double>>();
            bool isLeftSideCompleted = false;
            foreach (var p in points)
            {
                if (p.Key <= middleX)
                {
                    leftHalfPoints.Add(new KeyValuePair<double, double>(p.Key, p.Value));
                }
                if (p.Key >= middleX && !isLeftSideCompleted)
                {
                    leftHalfPoints.Add(crossPoint_1);
                    leftHalfPoints.Add(crossPoint_2);
                    isLeftSideCompleted = true;
                }
            }
            //ищем половину площади многоугольника по формуле Гаусса
            double calculatedHalfSquare = CalculatePolygonSquare(leftHalfPoints);
            var diff = calculatedHalfSquare - halfSquare;
            if (Math.Abs(diff) <= desiredAccuracy)
            {
                return middleX;
            }
            else 
            {
                // TODO: возможно над шагом нужно подумать дольше, если брать 1/10, то приближение идёт очень долго, но
                // малые значения могут быть опасны (можно перескочить, по крайней мере на первом шаге)
                double stepValue = Math.Abs(diff / 5); 
                // TODO: тут можно убрать рекурсию, т.к. предыдущее состояние нам по сути не нужно - достаточно возвращать
                // из функции не конечный ответ, а лишь очередное приближение и разность с ожидаемой величиной (в out-параметре),
                // а является оно ответом или нет, определять во внешнем коде и там же соот-но рассчитывать параметр startX.
                return GetXCoordOfHalfLine(points, halfSquare, diff < 0 ? middleX + stepValue : middleX - stepValue);
            }
        }

        /// <summary>
        /// Метод расчёта площади многоугольника по формуле "Шнуровки" Гаусса
        /// </summary>
        /// <param name="polyPoints"></param>
        /// <returns></returns>
        public static double CalculatePolygonSquare(IList<KeyValuePair<double, double>> polyPoints)
        {
            double square = 0;
            for (int i = 0; i < polyPoints.Count; i++)
            {
                int y1_Index = i + 1 < polyPoints.Count ? i + 1 : 0;
                int y2_Index = i - 1 >= 0 ? i - 1 : polyPoints.Count - 1;
                square += polyPoints[i].Key * (polyPoints[y1_Index].Value - polyPoints[y2_Index].Value);
            }
            return Math.Abs(square) / 2;
        }

        private static double CalculateLinear_Y(double middleX, KeyValuePair<int, int> leftPoint, KeyValuePair<int, int> rightPoint)
        {
            // ищем Y из уравнения прямой, порходящей через 2 точки: y = (x - x1)(y2 - y1) / (x2 - x1) + y1;
            double mY = (middleX - leftPoint.Key) * (rightPoint.Value - leftPoint.Value) / (rightPoint.Key - leftPoint.Key) + 
                leftPoint.Value;
            return mY;
        }

        /// <summary>
        /// Метод парсинга одной целочисленной координаты
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Когда координаты не являются целыми числами, не превосходящие по модулю ﻿﻿10^3 
        /// </exception>
        private static int ParseCoord(string value)
        {
            int res = int.Parse(value.Trim());
            if (Math.Abs(res) > 1000)
            {
                throw new ArgumentOutOfRangeException("Координаты должны быть целыми числами, не превосходящие по модулю ﻿﻿10^3 !");
            }
            return res;
        }
    }
}
