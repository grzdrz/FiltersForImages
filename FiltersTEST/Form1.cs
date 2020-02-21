using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FiltersTEST.Filters;
using FiltersTEST.ImageData;

namespace FiltersTEST
{
    public partial class Form1 : Form
    {
        public Bitmap bitmap1;
        public Bitmap bitmap2;
        Pixel[,] pixelsArray1;

        public Form1()
        {
            InitializeComponent();

            button1_ApplyFilter.Click += button1_Click_ApplyFilter;
            button2_SaveImage.Click += button2_Click_SaveImage;
            button3_LoadImage.Click += button3_Click_LoadImage;
            button4_FilterOptions.Click += button4_Click_FiltersOptions;
            button1_ApplyFilter.Enabled = false;
            button2_SaveImage.Enabled = false;
            button4_FilterOptions.Enabled = false;

            comboBox1.DataSource = new List<Filter> 
            {
                new GrayShadesFilter(),
                new ThresholdFilter(),
                new MedianFilter(), 
                new SobelFilter(),
                new MatrixFilter()
            };
            comboBox1.DisplayMember = "FilterName";
        }

        private void button1_Click_ApplyFilter(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is null) return;

            Cursor.Current = Cursors.WaitCursor;
            DisableAllButtons();

            Filter selectedFilter = (Filter)comboBox1.SelectedItem;
            bitmap2 = selectedFilter.ApplyFilter(pixelsArray1);
            pictureBox2.Image = bitmap2;

            button2_SaveImage.Enabled = true;

            Cursor.Current = Cursors.Default;
            button1_ApplyFilter.Enabled = true;
            button4_FilterOptions.Enabled = true;
            button2_SaveImage.Enabled = true;
            button3_LoadImage.Enabled = true;
        }

        private void button2_Click_SaveImage(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.AddExtension = true;
            saveFileDialog.Filter = "PNG files (*.png)|*.png";

            DialogResult showDialogResult = saveFileDialog.ShowDialog();
            if (showDialogResult == DialogResult.Cancel)
                return;
            if (showDialogResult == System.Windows.Forms.DialogResult.OK)
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    bitmap2.Save(stream, ImageFormat.Png);
                }
            MessageBox.Show("Файл сохранен");
        }

        private void button3_Click_LoadImage(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Filter = "PNG files (*.png)|*.png|JPEG files (*.jpg)|*.jpg|BMP files (*.bmp)|*.bmp";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                DisableAllButtons();

                bitmap1 = (Bitmap)Bitmap.FromFile(openFileDialog.FileName);
                pictureBox1.Image = bitmap1;
                pixelsArray1 = bitmap1.BitmapToPixelsArray();

                Cursor.Current = Cursors.Default;
                button1_ApplyFilter.Enabled = true;
                button4_FilterOptions.Enabled = true;
                button3_LoadImage.Enabled = true;
            }
        }

        Form2 Form2;
        private void button4_Click_FiltersOptions(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is null) return;
            //if (!(comboBox1.SelectedItem is IFilterOptionControls)) return;

            Filter selectedFilter = (Filter)comboBox1.SelectedItem;
            Form2 = new Form2(this, selectedFilter);
            Form2.Show();
            this.Enabled = false;
        }

        private void DisableAllButtons()
        {
            button1_ApplyFilter.Enabled = false;
            button2_SaveImage.Enabled = false;
            button3_LoadImage.Enabled = false;
            button4_FilterOptions.Enabled = false;
        }
    }
}
