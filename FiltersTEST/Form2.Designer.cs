namespace FiltersTEST
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1_Cancle = new System.Windows.Forms.Button();
            this.button2_Accept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1_Cancle
            // 
            this.button1_Cancle.Location = new System.Drawing.Point(219, 255);
            this.button1_Cancle.Name = "button1_Cancle";
            this.button1_Cancle.Size = new System.Drawing.Size(75, 23);
            this.button1_Cancle.TabIndex = 1;
            this.button1_Cancle.Text = "Cancle";
            this.button1_Cancle.UseVisualStyleBackColor = true;
            // 
            // button2_Accept
            // 
            this.button2_Accept.Location = new System.Drawing.Point(123, 255);
            this.button2_Accept.Name = "button2_Accept";
            this.button2_Accept.Size = new System.Drawing.Size(75, 23);
            this.button2_Accept.TabIndex = 2;
            this.button2_Accept.Text = "Accept";
            this.button2_Accept.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 290);
            this.Controls.Add(this.button2_Accept);
            this.Controls.Add(this.button1_Cancle);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1_Cancle;
        private System.Windows.Forms.Button button2_Accept;
    }
}