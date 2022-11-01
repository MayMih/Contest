using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_10
{
    internal static class Program
    {
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
                double res = GetXCoordOfHalfLine(points);
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
        private static double GetXCoordOfHalfLine(IList<KeyValuePair<int, int>> points, double desiredAccuracy = 0.000_001)
        {
            // ищем половину площади многоугольника по формуле Гаусса
            double halfSquare = 0;
            for (int i = 0; i < points.Count; i++)
            {
                int y1_Index = i + 1 < points.Count ? i + 1 : 0;
                int y2_Index = i - 1 > 0 ? i - 1 : points.Count - 1;
                halfSquare += points[i].Key * (points[y1_Index].Value - points[y2_Index].Value);
            }            
            halfSquare = Math.Abs(halfSquare) / 4;
            // пробуем найти простое решение - ищем координату половины ширины фигуры по оси Х
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
            double middleX = (max_X - min_X) / 2;
            double? middleY1 = null, middleY2 = null;
            // проверяем предельный случай, когда вершины попадают в существующие.            
            var knownCrossPoints = points.Where(y => Math.Abs(middleX - y.Key) <= desiredAccuracy);
            if (knownCrossPoints.GetEnumerator().MoveNext())
            {
                middleY1 = knownCrossPoints.GetEnumerator().Current.Value;
            }
            if (knownCrossPoints.GetEnumerator().MoveNext())
            {
                middleY2 = knownCrossPoints.GetEnumerator().Current.Value;
            }                                    
            // ищем 2 (или 4) ближайшие вершины между которыми попала точка middleX (снизу и сверху)
            KeyValuePair<int, int>? leftPoint_1 = null, leftPoint_2 = null;
            KeyValuePair<int, int>? rightPoint_1 = null, rightPoint_2 = null;
            // если среди вершин сразу средний Y найти не удалось, то ищем верхнюю и нижнюю грани выпуклого многоугольника,
            //      по которым идёт пересечение линией проведённой через предполагаемый "средний" X.
            if (!middleY1.HasValue || !middleY2.HasValue)
            {
                var foundPoint = FindNearestPoint(points, middleX);
                if (foundPoint.Key > middleX)
                {
                    rightPoint_1 = foundPoint;
                }
                else
                {
                    leftPoint_1 = foundPoint;
                }
                // т.к. точки идут подряд (по час. стрелке), мы можем, найдя одну ближайшую к пересечению точку, найти
                // вторую просто сдвиув индекс первой на 1 вправо или влево соот-но.
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].Key == foundPoint.Key && points[i].Value == foundPoint.Value)
                    {
                        if (leftPoint_1.HasValue)
                        {
                            rightPoint_1 = i < (points.Count - 1) ? points[i + 1] : points[0];
                            break;
                        }
                        else if (rightPoint_1.HasValue)
                        {
                            leftPoint_1 = i > 0 ? points[i - 1] : points[points.Count - 1];
                            break;
                        }
                    }
                }              
                // ищем Y из уравнения прямой, порходящей через 2 точки: y = (x - x1)(y2 - y1) / (x2 - x1) + y1;
                double mY = (middleX - leftPoint_1.Value.Key) * (rightPoint_1.Value.Value - leftPoint_1.Value.Value) / 
                    (rightPoint_1.Value.Key - leftPoint_1.Value.Key) + leftPoint_1.Value.Value;

                if (!middleY1.HasValue)
                {
                    middleY1 = mY;
                }
                else if (!middleY2.HasValue)
                {
                    middleY2 = mY;
                }
                // если мы нашли ещё не все точки пересечения, то ищем оставшиеся
                for (int i = 0; i < points.Count && !middleY1.HasValue && !middleY2.HasValue; i++)
                {
                    if (points[i].Key < middleX && (i != points.Count - 1) && points[i + 1].Key > middleX)
                    {
                        leftPoint_1 = points[i];
                        rightPoint_1 = points[i + 1];
                    }
                    else
                    {
                        int nexPointIndex = i + 1 != points.Count ? i + 1 : 0;
                        if (points[i].Key > middleX && points[nexPointIndex].Key < middleX)
                        {
                            leftPoint_2 = points[nexPointIndex];
                            rightPoint_2 = points[i];
                        }
                    }
                }
            }
            // точку(и) пересечения будем считать по параметрическому(им) уравнению(ям) прямой(ых) (см. http://www.cleverstudents.ru/line_and_plane/line_passes_through_2_points.html)
            var linePoint_1 = new KeyValuePair<double, double>(middleX, middleY1.Value);
            var linePoint_2 = new KeyValuePair<double, double>(middleX, middleY2.Value);            
            double calculatedHalfSquare = 0;
            // считаем половину площади с учётом двух новых точек
            // <...>
            if (calculatedHalfSquare - halfSquare <= desiredAccuracy)
            {
                return calculatedHalfSquare;
            }
        }

        private static bool isAllPointsFound()
        {
            throw new NotImplementedException();
        }

        private static KeyValuePair<int, int> FindNearestPoint(IEnumerable<KeyValuePair<int, int>> knownPoints, double middleX)
        {
            KeyValuePair<int, int> nearestPoint;
            double minDif = double.MaxValue;
            double curDif;
            int key = 0, value = 0;
            foreach (var p in knownPoints)
            {
                curDif = Math.Abs(p.Key - middleX);
                if (curDif < minDif)
                {
                    minDif = curDif;
                    key = p.Key;
                    value = p.Value;
                }
            }
            nearestPoint = new KeyValuePair<int, int>(key, value);
            return nearestPoint;
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
