using System;
using System.Collections;
using System.Numerics;

namespace Lab5DP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BigIntGenerator generator = new BigIntGenerator();
            // 199991543
            BigInteger integer = BigInteger.Parse("193");
            bool res = generator.IsPrime(integer);
            if (res)
            {
                Console.WriteLine($"{integer} простое");
            }
            else
            {
                Console.WriteLine($"{integer} не простое");
            }

            int l = 5;
            BigInteger big = generator.GenerateBigPrimeNumber(l);
            Console.WriteLine($"Сгенерированное простое число длиной {l} - {big}");
        }
    }
}