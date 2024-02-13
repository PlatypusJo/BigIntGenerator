using System;
using System.Collections;
using System.Numerics;

namespace Lab5DP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Nothing
            // 199991543
            //BigInteger integer = BigInteger.Parse("193");
            //bool res = generator.IsPrimeTest(integer);
            //if (res)
            //{
            //    Console.WriteLine($"{integer} простое");
            //}
            //else
            //{
            //    Console.WriteLine($"{integer} не простое");
            //}
            #endregion

            string filePath;
            StreamWriter writer;
            StreamReader reader;
            ConsoleKey chosen = ConsoleKey.None;
            Console.WriteLine("Сгенерировать число - G\n Проверить число - T\n Выйти - esc\n");

            while (chosen != ConsoleKey.Escape)
            {
                chosen = Console.ReadKey().Key;

                if (chosen == ConsoleKey.G)
                {
                    Console.WriteLine("\nВведите путь к файлу, в котором храниться длина числа: ");

                    filePath = Console.ReadLine();
                    reader = new StreamReader(File.Open(filePath, FileMode.Open));
                    string inputLength = reader.ReadToEnd();
                    reader.Close();

                    int l = Convert.ToInt32(inputLength);
                    BigInteger big = BigIntGenerator.GenerateBigPrimeNumber(l);

                    writer = new StreamWriter(File.Open("outputPrimeNumber.txt", FileMode.Create));
                    writer.WriteLine(big.ToString());
                    writer.Close();
                    Console.WriteLine($"Сгенерированное простое число длиной {l} - {big}");
                }
                else if (chosen == ConsoleKey.T)
                {
                    Console.WriteLine("\nВведите путь к файлу с числом: ");

                    filePath = Console.ReadLine();
                    reader = new StreamReader(File.Open(filePath, FileMode.Open));
                    string number = reader.ReadToEnd();
                    reader.Close();

                    BigInteger bigInteger = BigInteger.Parse(number);
                    string result;

                    if (BigIntGenerator.CheckNumberIsPrime(bigInteger))
                    {
                        result = $"Число {bigInteger} простое или псевдопростое";
                    }
                    else
                    {
                        result = $"Число {bigInteger} составное";
                    }

                    writer = new StreamWriter(File.Open("outputTestRes.txt", FileMode.Create));
                    writer.WriteLine(result);
                    writer.Close();
                    Console.WriteLine(result);
                }
            }
        }
    }
}