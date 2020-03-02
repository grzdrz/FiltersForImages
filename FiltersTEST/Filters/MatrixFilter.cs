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
            FilterOptions["Div"] = ((IMatrix)FilterOptions["MatrixType"]).DefaultDiv;
        }

        public override Bitmap ApplyFilter(Pixel[,] pixels)
        {
            double[,] matrix = ((IMatrix)FilterOptions["MatrixType"]).Matrix;
            double div = (double)FilterOptions["Div"] / 10d;
            double offset = ((IMatrix)FilterOptions["MatrixType"]).Offset;

            var width = pixels.GetLength(0);
            var height = pixels.GetLength(1);
            Bitmap result = new Bitmap(width, height);

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
                    double sum1R = 0; double sum1G = 0; double sum1B = 0;
                    for (int i = 0; i < matrix.GetLength(0); i++)
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            pixelsInOperatorXWindow[i, j] = pixels[x - matrix.GetLength(0) / 2 + i, y - matrix.GetLength(1) / 2 + j];
                            sum1R += pixelsInOperatorXWindow[i, j].R * matrix[i, j];
                            sum1G += pixelsInOperatorXWindow[i, j].G * matrix[i, j];
                            sum1B += pixelsInOperatorXWindow[i, j].B * matrix[i, j];
                        }
                    var resultR = FilterExtremeValues(sum1R / div + offset);
                    var resultG = FilterExtremeValues(sum1G / div + offset);
                    var resultB = FilterExtremeValues(sum1B / div + offset);
                    result.SetPixel(x, y, Color.FromArgb(pixels[x, y].A, (int)resultR, (int)resultG, (int)resultB));
                }
            return result;
        }

        public void CreateOptionControls(Form2 formOfOptions)
        {
            NumericUpDown numericTrackBar1_koef = new NumericUpDown();
            numericTrackBar1_koef.Value = Convert.ToDecimal(FilterOptions["Div"]);
            numericTrackBar1_koef.Maximum = 100;
            numericTrackBar1_koef.Minimum = 1;
            numericTrackBar1_koef.Name = "Div";
            numericTrackBar1_koef.Size = new Size(171, 20);////
            numericTrackBar1_koef.Location = new System.Drawing.Point(123, 10);////

            Label label_koef = new Label();
            label_koef.Font = new Font("Microsoft Sans Serif", 8F);
            label_koef.Text = "Tone koef";
            label_koef.Size = new Size(100, 20);/////
            label_koef.Location = new System.Drawing.Point(5, 10);//////

            ComboBox comboBox1_matrix = new ComboBox();
            comboBox1_matrix.Name = "MatrixType";
            comboBox1_matrix.Size = new Size(171, 20);////
            comboBox1_matrix.Location = new System.Drawing.Point(123, 50);////
            comboBox1_matrix.DataSource = new List<IMatrix>
            {
                new BlurMatrix(),
                new SharpnessMatrix(),
                new NegativeMatrix(),
                new EmbossMatrix()
            };
            comboBox1_matrix.DisplayMember = "MatrixName";
            comboBox1_matrix.SelectedValueChanged += (sender, args) =>
            {
                //автоматически устанавливает дефолтное значение div(коэффициент тона) при смене фильтра
                numericTrackBar1_koef.Value = (decimal)((IMatrix)comboBox1_matrix.SelectedItem).DefaultDiv;
            };

            Label label_matrixType = new Label();
            label_matrixType.Font = new Font("Microsoft Sans Serif", 8F);
            label_matrixType.Text = "Matrix type";
            label_matrixType.Size = new Size(100, 20);/////
            label_matrixType.Location = new System.Drawing.Point(5, 50);//////

            formOfOptions.Controls.Add(numericTrackBar1_koef);
            formOfOptions.Controls.Add(label_koef);
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


//матрица-оператор
//double[,] sx = new double[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
//double[,] sx = new double[,] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
//double[,] sx = new double[,] { { -2, 0, 0 }, { 0, 1, 0 }, { 0, 0, 2 } };