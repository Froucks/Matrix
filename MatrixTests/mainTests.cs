using System;
using Xunit;

namespace MatrixTests
{
    public class mainTests
    {
        [Fact]
        public void operationMultipleCheck() // Проверяет матрицы 1 матрица как массив, другая 
        {
            Matrix<int> trueMatrixA = new Matrix<int>(5, 5, (i, j) => i + j);
            Matrix<int> trueMatrixB = new Matrix<int>(5, 5, (i, j) => j + i);
            Matrix<int> trueMatrixC;
            int[,] normalMatrixA = new int[5, 5];
            int[,] normalMatrixB = new int[5, 5];
            int[,] normalMatrixC = new int[5, 5];
            int[,] trueMatrixResult = new int[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    normalMatrixA[i, j] = i + j;
                    normalMatrixB[i, j] = j + i;
                }
            }


            trueMatrixC = trueMatrixA * trueMatrixB;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    trueMatrixResult[i, j] = trueMatrixC[i, j];
                    int sum = 0;
                    for (int k = 0; k < 5; k++)
                    {
                        sum += normalMatrixA[i, k] * normalMatrixB[k, j];
                    }
                    normalMatrixC[i, j] = sum;
                }
            }

            Assert.Equal(normalMatrixC, trueMatrixResult);
        }
        [Fact]
        public void operationSumCheck()
        {
            Matrix<int> trueMatrixA = new Matrix<int>(5, 5, (i, j) => i + j);
            Matrix<int> trueMatrixB = new Matrix<int>(5, 5, (i, j) => j + i);
            Matrix<int> trueMatrixC;
            int[,] normalMatrixA = new int[5, 5];
            int[,] normalMatrixB = new int[5, 5];
            int[,] normalMatrixC = new int[5, 5];
            int[,] trueMatrixResult = new int[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    normalMatrixA[i, j] = i + j;
                    normalMatrixB[i, j] = j + i;
                }
            }


            trueMatrixC = trueMatrixA + trueMatrixB;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    normalMatrixC[i, j] = normalMatrixA[i, j] + normalMatrixB[i, j];
                    trueMatrixResult[i, j] = trueMatrixC[i, j];
                }
            }

            Assert.Equal(normalMatrixC, trueMatrixResult);
        }
        [Fact]
        public void GenerateDoubleCheck() //Сравниваем генерацию с double
        {
            Matrix<double> myMatrix = new Matrix<double>(5, 5, (i, j) => i + j + 0.13);
            double[,] arrayMatrix = new double[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    arrayMatrix[i, j] = i + j + 0.13;
                }
            }
            double[,] arrayMyMatrix = new double[5, 5];


            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    arrayMyMatrix[i, j] = myMatrix[i, j];
                }
            }

            Assert.Equal(arrayMatrix, arrayMyMatrix);
        }
        [Fact]
        public void GenerateCheck() //Проверяем генерацию матрицы
        {
            Matrix<int> myMatrix = new Matrix<int>(5, 5, (i, j) => i + j);
            int[,] arrayMatrix = new int[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    arrayMatrix[i, j] = i + j;
                }
            }
            int[,] arrayMyMatrix = new int[5, 5];


            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    arrayMyMatrix[i, j] = myMatrix[i, j];
                }
            }

            Assert.Equal(arrayMatrix, arrayMyMatrix);
        }
        [Fact]
        public void MultipleIsNotSum() //Умножение не сумма
        {
            Matrix<int> MatrixA = new Matrix<int>(5, 5, (i, j) => i + j);
            Matrix<int> MatrixB = new Matrix<int>(5, 5, (i, j) => i * j);
            Matrix<int> MatrixSum;
            Matrix<int> MatrixMultiple;
            MatrixSum = MatrixA + MatrixB;
            MatrixMultiple = MatrixA * MatrixB;
            Assert.NotEqual(MatrixA, MatrixB);
        }
    }
}
