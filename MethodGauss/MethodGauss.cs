using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Collections.Generic;
using System.Linq;

namespace MethodGauss
{
    public class MethodGauss
    {
        static Random random = new Random();             

        public Matrix<double> Matrix { get; set; }
        public Vector<double> RightPart { get; set; }
        public Vector<double> Solutions { get; set; }

        public MethodGauss()
            : this(random.Next(2, 6))
        { }

        public MethodGauss(int rowCol)
            : this(rowCol, rowCol)
        { }

        public MethodGauss(int row, int col)
        {          
            Matrix = Matrix<double>.Build.Random(row, col);
            RightPart = Vector.Build.Random(row);
            Solutions = Vector.Build.Random(row);            
        }

        public MethodGauss(double[,] matrix, double[] rightPart)
        {           
            Matrix = DenseMatrix.OfArray(matrix);
            RightPart = Vector<double>.Build.DenseOfArray(rightPart);
            Solutions = Vector<double>.Build.DenseOfEnumerable(Enumerable.Repeat<double>(0, Matrix.RowCount));
        }

        //перебудова матриці так, щоб рядок з максимальним елементом у стовпці перемістився наверх
        public void RestructureRows(int index)
        {
            //знаходим максимальний, починаючи з наступного рядка
            double maxElement = Matrix[index, index];
            int maxElementIndex = index;
            for (var i = index + 1; i < Matrix.RowCount; i++)
            {
                if (Matrix[i, index] > maxElement)
                {
                    maxElement = Matrix[i, index];
                    maxElementIndex = i;
                }
            }

            //міняєм місцями з початковим
            if (maxElementIndex > index)
            {
                var temp = RightPart[maxElementIndex];
                RightPart[maxElementIndex] = RightPart[index];
                RightPart[index] = temp;

                for (var i = 0; i < Matrix.ColumnCount; i++)
                {
                    temp = Matrix[maxElementIndex, i];
                    Matrix[maxElementIndex, i] = Matrix[index, i];
                    Matrix[index, i] = temp;
                }
            }
            WriteOnConsole();
        }

        public void SolveMatrix()
        {
            if (Matrix.RowCount != Matrix.ColumnCount)
                throw new Exception("NoSolutions");
            //прямий хід
            for (var i = 0; i < Matrix.RowCount - 1; i++)
            {
                RestructureRows(i);
                WriteOnConsole();
                for (var j = i + 1; j < Matrix.RowCount; j++)
                {
                    if (Matrix[i, i].CompareTo(0) != 0)
                    {
                        double multElement = Matrix[j, i] / Matrix[i, i];
                        for (var k = i; k < Matrix.ColumnCount; k++)
                        {
                            Matrix[j, k] -= Matrix[i, k] * multElement;
                        }
                        RightPart[j] -= RightPart[i] * multElement;

                    }
                    WriteOnConsole();
                }
            }
            //обернений хід
            for (var i = Matrix.RowCount - 1; i >= 0; i--)
            {
                Solutions[i] = RightPart[i];

                for (var j = Matrix.RowCount - 1; j > i; j--)
                    Solutions[i] -= Matrix[i, j] * Solutions[j];

                if (Matrix[i, i].CompareTo(0) == 0)
                    if (RightPart[i].CompareTo(0) == 0)
                        throw new Exception("InfinitySolutions");
                    else
                        throw new Exception("NoSolutions");

                Solutions[i] /= Matrix[i, i];

            }
            WriteOnConsole();
        }

        public override string ToString()
        {            
            var s = "";
            for (var i = 0; i < Matrix.RowCount; i++)
            {
                s += "\r\n";
                for (var j = 0; j < Matrix.ColumnCount; j++)
                {
                    s += Matrix[i, j].ToString("F0") + "\t";
                }
                s += "|\t" + RightPart[i].ToString("F02");
            }
            s += "\nSolutions:\n";
            foreach (var solution in Solutions)
            {
                s += solution.ToString("F08") + "\n";
            }

            return s;
        }

        public void WriteOnConsole()
        {
            Console.WriteLine(this.ToString());

        }
    }

}
