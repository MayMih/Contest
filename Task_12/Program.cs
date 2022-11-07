using System;
using System.Collections.Generic;
using System.Linq;


namespace Task_12
{
    /// <summary>
    /// Задача 12. "Сумма из трёх монет" (псевдокомбинаторика с ограничением на макс. сумму)
    /// </summary>
    internal static class Program
    {
        const uint MAX_COIN_VALUE = 100_000;
        const ulong MAX_SUM_LIMIT = 1_000_000_000_000_000_000;

        static void Main()
        {
            Console.WriteLine("Введите предельную стоимость всех монет в кошельке (1 <= N <= {0}):", MAX_SUM_LIMIT);
            ulong sumLimit = ulong.Parse(Console.ReadLine().Trim());
            if (sumLimit < 1 || sumLimit > MAX_SUM_LIMIT)
            {
                throw new ArgumentOutOfRangeException($"Предельно допустимая стоимость монет в кошельке должна быть (1 <= N <= {MAX_SUM_LIMIT})!");
            }
            Console.WriteLine("Введите через пробел три номинала монет, из которых будут составлены суммы (1 <= X <= {0}):", MAX_COIN_VALUE);
            var coinTypes = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => {
                var ct = uint.Parse(x);
                if (ct < 1 || ct > MAX_COIN_VALUE)
                {
                    throw new ArgumentOutOfRangeException($"Номинал монеты должен быть в пределах (1 <= X <= {MAX_COIN_VALUE})!");
                }
                return ct;
            }).ToArray();

            if (coinTypes.Length != coinTypes.Select(x => x).Distinct().Count())
            {
                throw new ArgumentException("Все монеты должны быть разных номиналов!");
            }
            var res = CalculateSumsCount(coinTypes, sumLimit);
            Console.WriteLine(res);
            Console.ReadKey();
        }

        private static ulong CalculateSumsCount(uint[] rawCoinTypes, ulong maxSumLimit)
        {
            if (maxSumLimit == 1)
            {
                return 1;
            }
            else if (rawCoinTypes.Contains(1u))
            {
                return maxSumLimit;
            }
            // удаляем монеты номинал которых кратен другой монете, т.к. это означает, что все возможные суммы с ней уже учтены
            var coinTypes = new List<uint>();
            foreach (var coinType in rawCoinTypes.OrderBy(x => x))
            {
                if (!rawCoinTypes.Any(x => x != coinType && x % coinType == 0))
                {
                    coinTypes.Add(coinType);
                }
            }
            var knownSums = new List<ulong>();
            for (int i = 0; i < coinTypes.Count; i++)
            {
                // сначала для каждой монеты считаем кол-во сумм только из этой монеты
                for (ulong curSum = 1; curSum <= maxSumLimit; curSum += coinTypes[i])
                {
                    knownSums.Add(curSum);
                }
                // затем начинаем поочереди добавлять следующие монеты к текущей
                for (int j = i + 1; j < coinTypes.Count; j++)
                {
                    for (ulong curSum = 1 + coinTypes[i]; curSum <= maxSumLimit; curSum += coinTypes[j])
                    {
                        knownSums.Add(curSum);
                    }
                }
            }
            return (ulong)knownSums.Select(x => x).Distinct().LongCount();
        }
    }
}
