using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MethodGauss
{
    public class MethodGauss
    {
            static Random random = new Random();

            public int RowCount { get; set; }
            public int ColCount { get; set; }
            public double[,] Matrix { get; set; }
            public double[] RightPart { get; set; }
            public double[] Solutions { get; set; }

            public MethodGauss()
                : this(random.Next(2, 6))
            { }

            public MethodGauss(int rowCol)
                : this(rowCol, rowCol)
            { }

            public MethodGauss(int row, int col)
            {
                RowCount = row;
                ColCount = col;
                Matrix = new double[RowCount, ColCount];
                RightPart = new double[RowCount];
                Solutions = new double[RowCount];

                for (var i = 0; i < RowCount; i++)
                {
                    RightPart[i] = random.Next(2, 10);
                    Solutions[i] = 0;
                    for (var j = 0; j < ColCount; j++)
                    {
                        Matrix[i, j] = random.Next(0, 10);
                    }
                }
            }


            //перебудова матриці так, щоб рядок з максимальним елементом у стовпці перемістився наверх
            public void RestructureRows(int index)
            {
                //знаходим максимальний, починаючи з наступного рядка
                double maxElement = Matrix[index, index];
                int maxElementIndex = index;
                for (var i = index + 1; i < RowCount; i++)
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

                    for (var i = 0; i < ColCount; i++)
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
                if (RowCount != ColCount)
                    throw new Exception("NoSolutions");
                //прямий хід
                for (var i = 0; i < RowCount - 1; i++)
                {
                    RestructureRows(i);
                    WriteOnConsole();
                    for (var j = i + 1; j < RowCount; j++)
                    {
                        if (Matrix[i, i].CompareTo(0) != 0)
                        {
                            double multElement = Matrix[j, i] / Matrix[i, i];
                            for (var k = i; k < ColCount; k++)
                            {
                                Matrix[j, k] -= Matrix[i, k] * multElement;
                            }
                            RightPart[j] -= RightPart[i] * multElement;

                        }
                        WriteOnConsole();
                    }
                }
                //обернений хід
                for (var i = RowCount - 1; i >= 0; i--)
                {
                    Solutions[i] = RightPart[i];

                    for (var j = RowCount - 1; j > i; j--)
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
                for (var i = 0; i < RowCount; i++)
                {
                    s += "\r\n";
                    for (var j = 0; j < ColCount; j++)
                    {
                        s += Matrix[i, j].ToString("F0") + "\t";
                    }
                    s += "|\t" + RightPart[i].ToString("F04");
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
