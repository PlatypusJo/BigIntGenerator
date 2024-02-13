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

            string lengthFile = "length.txt";
            string numberFile = "big.txt";
            string inputLength = new StreamReader(lengthFile).ReadToEnd();
            int l = Convert.ToInt32(inputLength);
            BigInteger big = BigIntGenerator.GenerateBigPrimeNumber(l);

            StreamWriter writer = new StreamWriter(File.Open("outputPrimeNumber.txt", FileMode.Create));
            writer.WriteLine(big.ToString());
            writer.Close();
            Console.WriteLine($"Сгенерированное простое число длиной {l} - {big}");

            string number = new StreamReader(numberFile).ReadToEnd();
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