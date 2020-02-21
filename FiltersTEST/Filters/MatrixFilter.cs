using FiltersTEST.ImageData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FiltersTEST.Filters.Matrixs.Base;
using FiltersTEST.Filters.Matrixs;

namespace FiltersTEST.Filters
{
    public class MatrixFilter : Filter, IFilterOptionControls
    {
        public override string FilterName { get { return "Matrix"; } }

        public override Dictionary<string, object> FilterOptions { get; set; }

        public MatrixFilter()
        {
            //дефолтные параметры фильтра
            FilterOptions = new Dictionary<string, object>();
            FilterOptions["MatrixType"] = new BlurMatrix();
            FilterOptions["Koef"] = 10d;
            FilterOptions["Koef2"] = 0d;
        }

        public override Bitmap ApplyFilter(Pixel[,] pixels)
        {
            //матрица-оператор
            //double[,] sx = new double[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
            //double[,] sx = new double[,] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
            //double[,] sx = new double[,] { { -2, 0, 0 }, { 0, 1, 0 }, { 0, 0, 2 } };

            //увеличение четкости
            //double[,] sx = new double[,] { { -1, -1, -1 }, { -1, 9, -1 }, { -1, -1, -1 } };

            //double[,] matrix = new double[,] {
            //    { 0, 0, 1, 0, 0 },
            //    { 0, 1, 1, 1, 0 },
            //    { 1, 1, 1, 1, 1 },
            //    { 0, 1, 1, 1, 0 },
            //    { 0, 0, 1, 0, 0 }
            //};

            //double koef = 1d;

            double[,] matrix = ((IMatrix)FilterOptions["MatrixType"]).Matrix;
            double koef = (double)FilterOptions["Koef"] / 10d;
            double koef2 = (double)FilterOptions["Koef2"];

            var width = pixels.GetLength(0);
            var height = pixels.GetLength(1);
            Bitmap result = new Bitmap(width, height);
            double[,] sy = new double[matrix.GetLength(1), matrix.GetLength(0)];

            //транспонирование матрицы-оператора
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    sy[j, i] = matrix[i, j];

            if (pixels.Length == 1)
            {
                var gray = (int)(255 * Math.Round(0.299 * pixels[0, 0].R + 0.587 * pixels[0, 0].G + 0.114 * pixels[0, 0].B, 10) / 255);
                gray = Math.Min(gray, 255);
                gray = Math.Max(gray, 0);

                int temp = (int)Math.Sqrt(
                    (double)gray * matrix[0, 0] * (double)gray * matrix[0, 0] +
                    (double)gray * matrix[0, 0] * (double)gray * matrix[0, 0]);
                result.SetPixel(0, 0, Color.FromArgb(pixels[0, 0].A, temp, temp, temp));
                return result;
            }

            for (int x = matrix.GetLength(0) / 2; x < width - matrix.GetLength(0) / 2; x++)
                for (int y = matrix.GetLength(1) / 2; y < height - matrix.GetLength(1) / 2; y++)
                {
                    //кусок изображения вокруг текущего(x, y) пикселя, размером операторной матрицы
                    Pixel[,] pixelsInOperatorXWindow = new Pixel[matrix.GetLength(0), matrix.GetLength(1)];
                    Pixel[,] pixelsInOperatorYWindow = new Pixel[sy.GetLength(0), sy.GetLength(1)];
                    double sum1R = 0; double sum1G = 0; double sum1B = 0;
                    //double sum2R = 0; double sum2G = 0; double sum2B = 0;
                    for (int i = 0; i < matrix.GetLength(0); i++)
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            pixelsInOperatorXWindow[i, j] = pixels[x - matrix.GetLength(0) / 2 + i, y - matrix.GetLength(1) / 2 + j];
                            sum1R += pixelsInOperatorXWindow[i, j].R * matrix[i, j];
                            sum1G += pixelsInOperatorXWindow[i, j].G * matrix[i, j];
                            sum1B += pixelsInOperatorXWindow[i, j].B * matrix[i, j];
                        }
                    //for (int i = 0; i < sy.GetLength(0); i++)
                    //    for (int j = 0; j < sy.GetLength(1); j++)
                    //    {
                    //        pixelsInOperatorYWindow[i, j] = pixels[x - sy.GetLength(0) / 2 + i, y - sy.GetLength(1) / 2 + j];
                    //        sum2R += pixelsInOperatorYWindow[i, j].R * sy[i, j];
                    //        sum2G += pixelsInOperatorYWindow[i, j].G * sy[i, j];
                    //        sum2B += pixelsInOperatorYWindow[i, j].B * sy[i, j];
                    //    }
                    //var resultR = FilterExtremeValues(Math.Sqrt(sum1R * sum1R + sum2R * sum2R));
                    //var resultG = FilterExtremeValues(Math.Sqrt(sum1G * sum1G + sum2G * sum2G));
                    //var resultB = FilterExtremeValues(Math.Sqrt(sum1B * sum1B + sum2B * sum2B));
                    var resultR = FilterExtremeValues(sum1R / koef + koef2);
                    var resultG = FilterExtremeValues(sum1G / koef + koef2);
                    var resultB = FilterExtremeValues(sum1B / koef + koef2);
                    result.SetPixel(x, y, Color.FromArgb(pixels[x, y].A, (int)resultR, (int)resultG, (int)resultB));
                }
            return result;
        }

        public void CreateOptionControls(Form2 formOfOptions)
        {
            NumericUpDown numericTrackBar1_koef = new NumericUpDown();
            numericTrackBar1_koef.Value = Convert.ToDecimal(FilterOptions["Koef"]);
            numericTrackBar1_koef.Maximum = 100;
            numericTrackBar1_koef.Minimum = 1;
            numericTrackBar1_koef.Name = "Koef";
            numericTrackBar1_koef.Size = new Size(171, 20);////
            numericTrackBar1_koef.Location = new System.Drawing.Point(123, 10);////

            Label label_koef = new Label();
            label_koef.Font = new Font("Microsoft Sans Serif", 8F);
            label_koef.Text = "Koef";
            label_koef.Size = new Size(100, 20);/////
            label_koef.Location = new System.Drawing.Point(5, 10);//////

            NumericUpDown numericTrackBar1_koef2 = new NumericUpDown();
            numericTrackBar1_koef2.Maximum = 256;
            numericTrackBar1_koef2.Minimum = 0;
            numericTrackBar1_koef2.Value = Convert.ToDecimal(FilterOptions["Koef2"]);
            numericTrackBar1_koef2.Name = "Koef2";
            numericTrackBar1_koef2.Size = new Size(171, 20);////
            numericTrackBar1_koef2.Location = new System.Drawing.Point(123, 60);////

            Label label_koef2 = new Label();
            label_koef2.Font = new Font("Microsoft Sans Serif", 8F);
            label_koef2.Text = "Koef2";
            label_koef2.Size = new Size(100, 20);/////
            label_koef2.Location = new System.Drawing.Point(5, 60);//////

            ComboBox comboBox1_matrix = new ComboBox();
            comboBox1_matrix.Name = "MatrixType";
            comboBox1_matrix.Size = new Size(171, 20);////
            comboBox1_matrix.Location = new System.Drawing.Point(123, 110);////
            comboBox1_matrix.DataSource = new List<IMatrix>
            {
                new BlurMatrix(),
                new SharpnessMatrix(),
                new TESTMatrix()
            };
            comboBox1_matrix.DisplayMember = "MatrixName";
            //comboBox1_matrix.SelectedItem = (IMatrix)FilterOptions["MatrixType"];

            Label label_matrixType = new Label();
            label_matrixType.Font = new Font("Microsoft Sans Serif", 8F);
            label_matrixType.Text = "Matrix type";
            label_matrixType.Size = new Size(100, 20);/////
            label_matrixType.Location = new System.Drawing.Point(5, 110);//////

            formOfOptions.Controls.Add(numericTrackBar1_koef);
            formOfOptions.Controls.Add(label_koef);
            formOfOptions.Controls.Add(numericTrackBar1_koef2);
            formOfOptions.Controls.Add(label_koef2);
            formOfOptions.Controls.Add(comboBox1_matrix);
            formOfOptions.Controls.Add(label_matrixType);
        }

        public double FilterExtremeValues(double pixel)
        {
            var gray = Math.Min(pixel, 255);
            gray = Math.Max(gray, 0);
            return gray;
        }

        public Pixel MultiplyPixelAndNumber(Pixel pixel, double number)
        {
            byte r = (byte)FilterExtremeValues(pixel.R * number);
            byte g = (byte)FilterExtremeValues(pixel.G * number);
            byte b = (byte)FilterExtremeValues(pixel.B * number);
            Pixel result = new Pixel(r, g, b, pixel.A, pixel.Position);

            return result;
        }
    }
}
