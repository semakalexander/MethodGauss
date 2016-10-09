using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MethodGauss
{
    class Program
    {
        static void Main(string[] args)
        {
            var mySolution = new GaussMethod(3, 3)
            {
                RightPart =
                {
                    [0] = 1,
                    [1] = 2,
                    [2] = 0
                },
                Matrix =
                {
                    [0, 0] = 4, [0, 1] = 2, [0, 2] = -1,
                    [1, 0] = 5, [1, 1] = 3, [1, 2] = -2,
                    [2, 0] = 3, [2, 1] = 2, [2, 2] = -3
                }
            };
            mySolution.SolveMatrix();
        }
    }
}
