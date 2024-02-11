using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab5DP
{
    public class BigIntGenerator
    {
        private List<BigInteger> _primeNumbers; // Список простых чисел до 5000.
        private int _maxIterations = 1000000; // Максимальное число итераций для метода проб (по умолчанию 1млн.).
        public int MaxIterations 
        {
            get => _maxIterations;
            set
            {
                _maxIterations = value;
            }
        }

        public BigIntGenerator()
        {
            // Создаю список простых чисел до 5000.
            _primeNumbers = new List<BigInteger>();
            for (int i = 0; i < 5000; i++)
            {
                _primeNumbers.Add(i + 2);
            }

            BigInteger div;
            for (int j = 0; j < _primeNumbers.Count; j++)
            {
                div = _primeNumbers[j];
                for (int i = j + 1; i < _primeNumbers.Count; i++)
                {
                    if (_primeNumbers[i] % div == 0)
                        _primeNumbers.RemoveAt(i);
                }
            }
        }

        public BigInteger GenerateBigNumber(int length)
        {
            if (length > 150)
                throw new ArgumentException("Длина должна быть не больше 150 символов");

            string number = new Random().Next(1, 10).ToString();
            for (int i = 1; i <= length; i++)
            {
                number += new Random().Next(0, 10).ToString();
            }

            return BigInteger.Parse(number);
        }
        
        public BigInteger GenerateBigPrimeNumber(int length)
        {
            if (length > 150)
                throw new ArgumentException("Длина должна быть не больше 150 символов");

            BigInteger bigPrime = GenerateBigNumber(length);
            if (bigPrime.IsEven)
                bigPrime++;

            return 0;
        }

        private bool DoMillerTest(BigInteger number)
        {
            return false;
        }

        private bool DoPrimeNumberTest(BigInteger number)
        {  
            return false;
        }

        public void CheckNumberIsPrime(BigInteger number)
        {

        }

        // Разложение расширенным методом проб (нужно добавить ограничение по итерациям)
        private List<BigInteger> TrialDivision(BigInteger n)
        {
            List<BigInteger> divides = [];
            int iterationCount = 0;
            BigInteger div = 2;

            while (n > 1 && iterationCount < _maxIterations)
            {
                if (n % div == 0)
                {
                    divides.Add(div);
                    n /= div;
                }
                else
                {
                    div++;
                }
            }

            // Удалить повторы
            divides = divides.Distinct().ToList();

            return divides;
        }
    }
}
