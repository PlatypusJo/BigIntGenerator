using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab5DP
{
    /// <summary>
    /// Класс для генерации больших чисел.
    /// </summary>
    public static class BigIntGenerator
    {
        private static List<BigInteger> _primeNumbers; // Список простых чисел до 5000.
        private static int _maxIterations = 10000000; // Максимальное число итераций для метода проб (по умолчанию 10млн.).
        public static int MaxIterations 
        {
            get => _maxIterations;
            set
            {
                _maxIterations = value;
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        static BigIntGenerator()
        {
            // Создаю список простых чисел до 5000.
            _primeNumbers = [];
            for (int i = 0; i < 5000; i++)
                _primeNumbers.Add(i + 2);

            BigInteger div;
            for (int j = 0; j < _primeNumbers.Count; j++)
            {
                div = _primeNumbers[j];
                for (int i = j + 1; i < _primeNumbers.Count; i++)
                    if (_primeNumbers[i] % div == 0)
                        _primeNumbers.RemoveAt(i);
            }
        }

        /// <summary>
        /// Метод генерации случайного числа заданной длины.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static BigInteger GenerateBigNumber(int length)
        {
            if (length > 1000)
                throw new ArgumentException("Длина должна быть не больше 1000 символов");

            string number = new Random().Next(1, 10).ToString();
            for (int i = 1; i < length; i++)
            {
                number += new Random().Next(0, 10).ToString();
            }

            return BigInteger.Parse(number);
        }
        
        /// <summary>
        /// Метод генерации случайного простого числа заданной длины.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static BigInteger GenerateBigPrimeNumber(int length)
        {
            if (length > 1000)
                throw new ArgumentException("Длина должна быть не больше 1000 символов");

            // Генерируем большое число
            BigInteger bigPrime = GenerateBigNumber(length);
            // Проверяем на чётность
            if (bigPrime.IsEven)
                bigPrime++;

            // Добавляем 2 к числу, пока не получим простое.
            bool isPrime = false;
            while (!isPrime)
            {
                isPrime = CheckNumberIsPrime(bigPrime);
                if (!isPrime)
                    bigPrime += 2;
            }

            return bigPrime;
        }

        /// <summary>
        /// Метод реализующий тест Миллера.
        /// </summary>
        /// <param name="number"> Число для проверки. </param>
        /// <param name="baseValue"> Основание. </param>
        /// <returns> Результат проверки. </returns>
        public static bool DoMillerTest(BigInteger number, BigInteger baseValue)
        {
            BigInteger buf = number - 1;
            BigInteger q = 0;
            BigInteger k = 0;

            while (buf > 1)
            {
                if (buf % 2 == 0)
                {
                    buf /= 2;
                    k++;
                }
                else
                {
                    q = buf;
                    break;
                }
            }

            int i = 0;
            BigInteger deduction = BigInteger.ModPow(baseValue, q, number);

            while (i < k)
            {
                if ((i == 0 && deduction == 1) || (i >= 0 && deduction == number - 1))
                {
                    Console.WriteLine($"Ничего определённого сказать нельзя. Основание: {baseValue}");
                    return false;
                }
                else
                {
                    i++;
                    deduction = BigInteger.ModPow(deduction, 2, number);
                }
                
            }

            Console.WriteLine($"Число {number} составное. Основание: {baseValue}");
            return true;
        }

        /// <summary>
        /// Тест на простоту.
        /// </summary>
        /// <param name="number"> Число для проверки. </param>
        /// <returns> Результат проверки. </returns>
        public static bool IsPrimeTest(BigInteger number)
        {  
            List<BigInteger> primeFactors = [];
            primeFactors = TrialDivision(number - 1);
            int i = 1;
            int r = primeFactors.Count;

            while (i <= r)
            {
                int iterationCount = 0;
                BigInteger b = 2;
                while (b < number && iterationCount < _maxIterations && !(BigInteger.ModPow(b, number - 1, number) == 1 && BigInteger.ModPow(b, (number - 1) / primeFactors[i - 1], number) != 1))
                {
                    b++;
                    iterationCount++;
                }

                if (b == number)
                {
                    return false;
                }
                i++;
            }
            return true;
        }

        /// <summary>
        /// Проверка на простоту.
        /// </summary>
        /// <param name="number"> Число для проверки. </param>
        /// <returns> Результат проверки. </returns>
        public static bool CheckNumberIsPrime(BigInteger number)
        {
            bool result = false;

            Console.WriteLine("Проверка на простоту делением на простые числа до 5000");
            for (int i = 0; i < _primeNumbers.Count; i++)
            {
                if (number % _primeNumbers[i] == 0 && number != _primeNumbers[i])
                {
                    Console.WriteLine($"Число составное. Делитель: {_primeNumbers[i]}");
                    return result;
                }
            }

            Console.WriteLine("Проверка на простоту тестом Миллера");
            for (int i = 0; i < 20; i++)
            {
                result = DoMillerTest(number, _primeNumbers[i]);
                if (result) 
                    return !result;
            }

            Console.WriteLine("Проверка на простоту тестом на простоту");
            result = IsPrimeTest(number);

            return result;
        }

        /// <summary>
        /// Разложение расширенным методом проб.
        /// </summary>
        /// <param name="n"> Число. </param>
        /// <returns> Список простых множителей. </returns>
        private static List<BigInteger> TrialDivision(BigInteger n)
        {
            List<BigInteger> divides = [];
            int iterationCount = 0;
            BigInteger div = 2;

            while (n % div == 0)
            {
                divides.Add(div);
                n /= div;
                Console.WriteLine(div);
                iterationCount++;
            }

            div = 3;

            while (BigInteger.Pow(div, 2) <= n && iterationCount < _maxIterations)
            {
                if (n % div == 0)
                {
                    divides.Add(div);
                    n /= div;
                    Console.WriteLine(div);
                }
                else
                {
                    div += 2;
                }
                iterationCount++;
            }

            if (n > 1)
            {
                Console.WriteLine(n);
                divides.Add(n);
            }

            return divides;
        }
    }
}
