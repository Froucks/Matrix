using System;
using System.IO;
using System.Text;


public class Matrix<T>
{

    private T[,] data;
    private int rows, cols; // Строки и столбцы

    public T this[int i, int j] // T Обобщенный тип
    {
        get => data[i, j];
        set => data[i, j] = value;
    }


    public Matrix(int _rows, int _cols) // Конструктор 1
    {
        rows = _rows;
        cols = _cols;
        data = new T[rows, cols];
    }

    public Matrix(int _rows, int _cols, Func<int, int, T> func) // Конструктор с делегатом Maintests
    {
        rows = _rows;
        cols = _cols;
        data = new T[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                this[i, j] = func(i, j); 
            }
        }
    }



    public void SaveToCSV(string pathName)
    {
        string result = "";
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result += this[i, j].ToString();
                if (j != cols - 1) result += ";";
            }
            result += "\n";
        }
        File.WriteAllText(pathName, result);
    }


    public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b) // Определение суммы матриц
    {
        if (a.cols != b.cols || a.rows != b.rows) throw new ArgumentException("Попытка суммировать матрицы разных размеров");
        else
        {
            Matrix<T> result = new Matrix<T>(a.rows, a.cols);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    result[i, j] = (dynamic)a[i, j] + (dynamic)b[i, j];
                }
            }
            return result;
        }
    }

    public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b) // Определение умножения матриц
    {
        if (a.cols != b.rows) throw new ArgumentException("Попытка умножения матриц некорректных размеров");
        else
        {
            int sums = a.cols;
            Matrix<T> result = new Matrix<T>(a.rows, b.cols);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    dynamic sum = 0;
                    for (int k = 0; k < sums; k++)
                    {
                        sum += (dynamic)a[i, k] * (dynamic)b[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }
    }
}

