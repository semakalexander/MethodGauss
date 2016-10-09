using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodGauss
{
    public class Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public Fraction()
            : this(1, 1)
        { }

        public Fraction(int numerator)
            : this(numerator, 1)
        { }

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new DivideByZeroException();
            this.Numerator = numerator;
            this.Denominator = denominator;
        }

        public static Fraction operator +(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction()
            {
                Numerator = fraction1.Numerator * fraction2.Denominator + fraction2.Numerator * fraction1.Denominator,
                Denominator = fraction1.Denominator * fraction2.Denominator
            }.Normalize();
        }

        public static Fraction operator -(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction()
            {
                Numerator = fraction1.Numerator * fraction2.Denominator - fraction2.Numerator * fraction1.Denominator,
                Denominator = fraction1.Denominator * fraction2.Denominator
            }.Normalize();
        }

        public static Fraction operator *(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction()
            {
                Numerator = fraction1.Numerator * fraction2.Numerator,
                Denominator = fraction1.Denominator * fraction2.Denominator
            }.Normalize();
        }

        public static Fraction operator /(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction()
            {
                Numerator = fraction1.Numerator * fraction2.Denominator,
                Denominator = fraction1.Denominator * fraction2.Numerator
            }.Normalize();
        }

        //зводим до спільного знаменника і порівнюєм чисельники
        public static bool operator >(Fraction fraction1, Fraction fraction2)
        {
            return fraction1.Numerator * fraction2.Denominator > fraction2.Numerator * fraction1.Denominator;
        }

        //зводим до спільного знаменника і порівнюєм чисельники
        public static bool operator <(Fraction fraction1, Fraction fraction2)
        {
            return fraction1.Numerator*fraction2.Denominator < fraction2.Numerator*fraction1.Denominator;
        }

      

        public static bool operator ==(Fraction fraction, int number)
        {
            if(fraction==null) throw new Exception("null");
            return fraction.Numerator/fraction.Denominator == number;
        }

        public static bool operator !=(Fraction fraction, int number)
        {
            if (fraction == null) throw new Exception("null");
            return fraction.Numerator / fraction.Denominator != number;
        }

        public Fraction Normalize()
        {
            var commonDivisor = GetCommonDivisor(this.Numerator, this.Denominator);
            return new Fraction()
            {
                Numerator = this.Numerator / commonDivisor,
                Denominator = this.Denominator / commonDivisor
            };
        }

        public static int GetCommonDivisor(int n, int d)
        {
            n = Math.Abs(n);
            d = Math.Abs(d);
            while (n != d)
                if (n > d)
                    n -= d;
                else
                    d -= n;
            return n;
        }


        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
    }
}
