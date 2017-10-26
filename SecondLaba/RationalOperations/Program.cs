using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RationalOperations
{
    public struct Rational
    {
        /// Числитель дроби
        public int Numerator { get; set; }
        /// Знаменатель дроби
        public int Denominator { get; set; }

        /// Целая часть числа Z.N:D, Z. получается делением числителя на знаменатель и
        /// отбрасыванием остатка
        public int Base 
        {
            get{ return Numerator / Denominator; } 
        }

        /// Дробная часть числа Z.N:D, N:D
        public int Fraction 
        {
            get { return Numerator % Denominator; } 
        }

        private static void WithoutIntegerPart(int intPart, ref Rational fraction)
        {
            fraction.Numerator = intPart * fraction.Denominator + fraction.Numerator;
        }

        private int GCD(int a, int b)
        {
            while (a != 0 && b != 0)

            {

                if (a >= b) a = a % b;

                else b = b % a;

            }

            return a + b;
        }

        /// Операция сложения, возвращает новый объект - рациональное число,
        /// которое является суммой чисел c и this
        public Rational Add(Rational c)
        {
            var newRational = c;

            if (this.Denominator != c.Denominator)
            {
                if (GCD(c.Denominator, this.Denominator) == 1)
                {
                    newRational.Denominator = this.Denominator * c.Denominator;
                }
                else
                {
                    newRational.Denominator = GCD(c.Denominator, this.Denominator);
                }


                newRational.Numerator = 
                    c.Numerator * (newRational.Denominator/c.Denominator) + 
                    this.Numerator * (newRational.Denominator/this.Denominator);
                return newRational;
            }

            newRational.Numerator = c.Numerator + this.Numerator;
            return newRational;
        }

        public Rational Sub(Rational c)
        {
            var tempC = new Rational()
            {
                Numerator = - c.Numerator,
                Denominator = c.Denominator
            };

            return this.Add(c);
        }
        /// Операция смены знака, возвращает новый объект - рациональное число,
        /// которое являтеся разностью числа 0 и this
        public Rational Negate()
        {
            var zeroRational = new Rational
            {
                Numerator = 0,
                Denominator = this.Denominator
            };

            return zeroRational.Sub(this);
        }

        /// Операция умножения, возвращает новый объект - рациональное число,
        /// которое является результатом умножения чисел x и this
        public Rational Multiply(Rational x)
        {
            var newRational = new Rational
            {
                Numerator = this.Numerator * x.Numerator,
                Denominator = this.Denominator * x.Denominator
            };

            return newRational;
        }

        /// Операция деления, возвращает новый объект - рациональное число,
        /// которое является результатом деления this на x
        public Rational DivideBy(Rational x)
        {
            var newRational = new Rational
            {
                Numerator = this.Numerator * x.Denominator,
                Denominator = this.Denominator * x.Numerator
            };

            return newRational;
        }

        /// Вовзращает строковое представление числа в виде Z.N:D, где
        /// Z — целая часть N и D — целые числа, числитель и знаменатель, N < D
        /// '.' — символ, отличающий целую часть от дробной,
        /// ':' — символ, обозначающий знак деления
        /// если числитель нацело делится на знаменатель, то
        /// строковое представление не отличается от целого числа
        /// (исчезает точка и всё, что после точки)
        /// Если Z = 0, опускается часть представления до точки включительно
        public override string ToString()
        {
            return string.Format("{0}.{1}:{2}", this.Base, this.Fraction, this.Denominator);
        }

        /// Создание экземпляра рационального числа из строкового представления Z.N:D
        /// допускается N > D, также допускается
        /// Строковое представления рационального числа
        /// Результат конвертации строкового представления в рациональное
        /// число
        /// true, если конвертация из строки в число была успешной,
        /// false если строка не соответствует формату
        public static bool TryParse(string input, out Rational result)
        {
           result = new Rational();

            if (input.Contains("."))
            {
                Regex valParseRegex = new Regex(@"^(\d+)[.](\d+)[:](\d+)?$", RegexOptions.IgnoreCase);

                var match = valParseRegex.Match(input);
                if (!match.Success)
                    return false;

                result.Numerator = Convert.ToInt32(match.Groups[2].Value);
                result.Denominator = Convert.ToInt32(match.Groups[3].Value);

                WithoutIntegerPart(Convert.ToInt32(match.Groups[1].Value), ref result);
            }
            else
            {
                Regex valParseRegex = new Regex(@"^(\d+)[:](\d+)?$", RegexOptions.IgnoreCase);

                var match = valParseRegex.Match(input);
                if (!match.Success)
                    return false;

                result.Numerator = Convert.ToInt32(match.Groups[1].Value);
                result.Denominator = Convert.ToInt32(match.Groups[2].Value);
            }

            return true;
        }

        /// Приведение дроби - сокращаем дробь на общие делители числителя
        /// и знаменателя. Вызывается реализацией после каждой арифметической операции
        private void Even()
        {
            var currGCD = GCD(this.Numerator, this.Denominator);

            this.Numerator /= currGCD;
            this.Denominator /= currGCD;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
