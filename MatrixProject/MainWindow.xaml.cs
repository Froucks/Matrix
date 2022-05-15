using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MatrixProject 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBox[,] leftMatrix, rightMatrix;
        private int matrixSizeNow;
        private Matrix<int> result;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnClearAndGenerate(object sender, RoutedEventArgs e) //обработчик кнопки clear
        {

            leftMatrix = GenerateGrid(LeftMatrix, int.Parse(sizeInit.Text));
            rightMatrix = GenerateGrid(RightMatrix, int.Parse(sizeInit.Text));
        }

        private TextBox[,] GenerateGrid(UniformGrid Grid, int size) // Создание поля для матриц
        {
            matrixSizeNow = size;
            TextBox[,] result = new TextBox[size, size];

            Grid.Rows = size;
            Grid.Columns = size;
            Grid.Children.Clear();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = new TextBox();
                    result[i, j].Text = "0";
                    Grid.Children.Add(result[i, j]);
                }
            }
            return result;
        }

        private void btnRandomValues(object sender, RoutedEventArgs e) // Обработчик кнопки случайных значений
        {
            randomValuesMatrix(leftMatrix);
            randomValuesMatrix(rightMatrix);
        }

        private void randomValuesMatrix(TextBox[,] textBoxes) // Случайные знчения
        {
            Random rand = new Random(DateTime.Now.Second + DateTime.Now.Millisecond);
            foreach (TextBox item in textBoxes)
            {
                item.Text = rand.Next(0, 10).ToString();
            }
        }


        private void btnResultValues(object sender, RoutedEventArgs e) 
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Matrix<int> matrixLeft = new Matrix<int>(matrixSizeNow, matrixSizeNow);
            Matrix<int> matrixRight = new Matrix<int>(matrixSizeNow, matrixSizeNow);
            for (int i = 0; i < matrixSizeNow; i++)//Запись в левую и правую матрицы значений из textbox
            {
                for (int j = 0; j < matrixSizeNow; j++)
                {
                    matrixLeft[i, j] = int.Parse(leftMatrix[i, j].Text);
                    matrixRight[i, j] = int.Parse(rightMatrix[i, j].Text);
                }
            }

            if (operationType.SelectedIndex == 0) result = matrixLeft + matrixRight; //Если 0 - сумма, если 1, то догадайся
            else result = matrixLeft * matrixRight;

            stopwatch.Stop();
            timeOutput.Text = stopwatch.Elapsed.TotalSeconds.ToString();
            TextBox[,] resultFields = GenerateGrid(ResultMatrix, matrixSizeNow); //Вывод значений в форму
            for (int i = 0; i < matrixSizeNow; i++)
                for (int j = 0; j < matrixSizeNow; j++)
                    resultFields[i, j].Text = result[i, j].ToString();
        }

        private void btnPrintToCSV(object sender, RoutedEventArgs e)
        {
            string path = GetPathFile();
            if (path != null) result.SaveToCSV(path);
        }
        public string GetPathFile()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Result"; // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "CSV Table (.csv)|*.csv"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                return dlg.FileName;
            }
            else return null;
        }

    }
}
