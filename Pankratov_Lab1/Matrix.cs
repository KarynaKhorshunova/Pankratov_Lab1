﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pankratov_Lab1
{
    public class Matrix
    {
        public double[][] MatrixArray { get; set; }

        public Matrix()
        {

        }

        public Matrix(int m, int n)
        {
            MatrixArray = new double[m][];

            for (int i = 0; i < m; i++)
            {
                MatrixArray[i] = new double[n];
            }
        }

        public Matrix(double[][] data)
        {
            MatrixArray = data;
        }

        public void Print()
        {
            foreach (var row in MatrixArray)
            {
                foreach (var cell in row)
                {
                    Console.Write(cell + "\t");
                }
                Console.WriteLine();
            }
        }


        public Matrix Compare(bool withEqual)
        {
            var result = new Matrix(MatrixArray.Length, MatrixArray[0].Length);
            int value = 1;

            for (int i = 0; i < MatrixArray.Length; i++)
            {
                for (int j = 0; j < MatrixArray[i].Length; j++)
                {
                    value = 1;
                    if (i != j)
                    {
                        var compare = MatrixArray[i][j].CompareTo(MatrixArray[j][i]);
                        if (withEqual)
                        {
                            value = compare > 0 ? 2 : (compare == 0 ? 1 : 0);

                        }
                        else
                        {
                            value = compare >= 0 ? 1 : 0;
                        }
                    }

                    result.MatrixArray[i][j] = value;
                }
            }

            return result;
        }

        public double[] SumRows()
        {
            double[] result = new double[MatrixArray.Length];

            for (int i = 0; i < MatrixArray.Length; i++)
            {
                result[i] = 0;
                foreach (var cell in MatrixArray[i])
                {
                    result[i] += cell;
                }
            }

            return result;
        }


        public double[] SumColumns()
        {
            double[] result = new double[MatrixArray[0].Length];

            for (int i = 0, j = 0; i < MatrixArray.Length; i++, j = 0)
            {
                foreach (var cell in MatrixArray[i])
                {
                    result[j++] += cell;
                }
            }

            return result;
        }

        public double Sum()
        {
            double sum = 0;
            foreach (var row in MatrixArray)
            {
                foreach (var cell in row)
                {
                    sum += cell;
                }
            }

            return sum;
        }

        public Matrix Normalize()
        {
            var result = new Matrix(MatrixArray.Length, MatrixArray[0].Length);
            double sum = Sum();

            for (int i = 0; i < MatrixArray.Length; i++)
            {
                for (int j = 0; j < MatrixArray[i].Length; j++)
                {
                    result.MatrixArray[i][j] = MatrixArray[i][j] / sum;
                }
            }

            return result;
        }

        public static Matrix Normalize(Matrix m)
        {
            var sum = m.Sum();
            var result = new Matrix(m.MatrixArray.Length, m.MatrixArray[0].Length);

            for (int i = 0; i < m.MatrixArray.Length; i++)
            {
                for (int j = 0; j < m.MatrixArray[i].Length; j++)
                {
                    result.MatrixArray[i][j] = m.MatrixArray[i][j] / sum;
                }
            }

            return result;
        }

        public Matrix CalculateWeightenedExpertMatrix()
        {
            int alternatives = MatrixArray[0].Length;

            var modifiedMatrix = new Matrix(MatrixArray.Length, MatrixArray[0].Length);

            for (int i = 0; i < MatrixArray.Length; i++)
            {
                for (int j = 0; j < MatrixArray[i].Length; j++)
                {
                    modifiedMatrix.MatrixArray[i][j] = alternatives - MatrixArray[i][j];
                }
            }

            return modifiedMatrix;
        }



        public double[] ExpertPreferred()
        {

            var weightenedMatrix = CalculateWeightenedExpertMatrix();
            var columnSums = weightenedMatrix.SumColumns();
            var totalSum = weightenedMatrix.Sum();

            var result = new double[columnSums.Length];

            for (int i = 0; i < columnSums.Length; i++)
            {
                result[i] = columnSums[i] / totalSum;
            }

            return result;
        }

    }
}