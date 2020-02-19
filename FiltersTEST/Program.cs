using FiltersTEST.Filters;
using FiltersTEST.ImageData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiltersTEST
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TEST();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void TEST()
        {
            Pixel[,] pixels = new Pixel[,]
            {
                { new Pixel(Color.White, new ImageData.Point(0, 0)), new Pixel(Color.Wheat, new ImageData.Point(0, 1)), new Pixel(Color.Red, new ImageData.Point(0, 2))},
                { new Pixel(Color.White, new ImageData.Point(1, 0)), new Pixel(Color.Wheat, new ImageData.Point(1, 1)), new Pixel(Color.Red, new ImageData.Point(1, 2))},
                { new Pixel(Color.White, new ImageData.Point(2, 0)), new Pixel(Color.Wheat, new ImageData.Point(2, 1)), new Pixel(Color.Red, new ImageData.Point(2, 2))}
            };

            var testFilter = new MedianFilter();
            var result = testFilter.ApplyFilter(pixels);
        }
    }
}
