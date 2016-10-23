using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
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
          
            var mySolution = new MethodGauss(new double[,] { 
                     { 4, 2, -1 },
                     { 5, 3, -2 },
                     { 3, 2, -3 }
            }, new double[] { 1, 2, 0 });

            Console.WriteLine(mySolution.Matrix);
            mySolution.RightPart = Vector<double>.Build.DenseOfArray(new double[] { 1, 2, 0 });
            

            var transp = mySolution.Matrix.Transpose();
            Console.WriteLine("Transponse: " + transp);
            Console.WriteLine("Determinant = " + mySolution.Matrix.Determinant());
            mySolution.SolveMatrix();
          
         
            Console.ReadLine();
        }
    }
}
