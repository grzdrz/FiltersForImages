using FiltersTEST.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiltersTEST
{
    public partial class Form2 : Form
    {
        public Form1 Form1;
        public Filter Filter;

        public Form2(Form1 form1, Filter filter)
        {
            Form1 = form1;
            Filter = filter;
            InitializeComponent();

            button1_Cancle.Click += button1_Click_Close;
            button2_Accept.Click += button2_Click_Accept;   

            if (filter is IFilterOptionControls)
                ((IFilterOptionControls)filter).CreateOptionControls(this);
        }

        private void button1_Click_Close(object sender, EventArgs e)
        {
            Form1.Enabled = true;
            this.Close();
        }

        private void button2_Click_Accept(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is NumericUpDown)
                {
                    NumericUpDown tempControl = (NumericUpDown)control;
                    Filter.FilterOptions[tempControl.Name] = (double)tempControl.Value;
                }
                if (control is TextBox)
                {
                    TextBox tempControl = (TextBox)control;
                    Filter.FilterOptions[tempControl.Name] = tempControl.Text;
                }
            }

            Form1.Enabled = true;
            this.Close();
        }
    }
}
