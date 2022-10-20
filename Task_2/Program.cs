using System;

namespace Task_2
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            string input = String.Empty;
            if (args.Length > 1)
            {
                throw new ArgumentException("Формат входных данных\r\n\r\nДано одно натуральное число  ﻿﻿N(1\\leq N\\leq 2 \\times10^9)N(1≤N≤2×10 \r\n9\r\n )﻿﻿ —  количество людей на кофе-брэйке.");
            }
            else if (args.Length == 1)
            {
                input = args[0];
            }
            else
            {
                input = Console.ReadLine();
            }
            uint partsCount = uint.Parse(input.Trim());
            if (partsCount < 1 || partsCount > 2_000_000_000)
            {
                throw new ArgumentException("Формат входных данных\r\n\r\nДано одно натуральное число  ﻿﻿N(1\\leq N\\leq 2 \\times10^9)N(1≤N≤2×10 \r\n9\r\n )﻿﻿ —  количество людей на кофе-брэйке.");
            }
            if (partsCount == 1)
            {
                Console.WriteLine(0);
            }
            else if (partsCount % 2 == 0)
            {
                Console.WriteLine(partsCount / 2);
            }
            else
            {
                Console.WriteLine((partsCount / 2) + 1);
            }
            Console.ReadKey();
        }
    }
}
