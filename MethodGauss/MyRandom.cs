using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodGauss
{
   public class MyRandom
    {
         static Random random = new Random();

        public static Fraction GetRandomValue()
        {
            return new Fraction(random.Next(), random.Next());
        }

        public static Fraction GetRandomValue(int min, int max)
        {
            return new Fraction(random.Next(min, max), random.Next(min, max));
        }
    }
}
