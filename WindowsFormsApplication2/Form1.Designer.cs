﻿namespace WindowsFormsApplication2
{
    partial class Form1
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
            this.drawMltiPicControl1 = new DrawMltPicControl.DrawMltiPicControl();
            this.SuspendLayout();
            // 
            // drawMltiPicControl1
            // 
            this.drawMltiPicControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawMltiPicControl1.Location = new System.Drawing.Point(0, 0);
            this.drawMltiPicControl1.Name = "drawMltiPicControl1";
            this.drawMltiPicControl1.Size = new System.Drawing.Size(732, 483);
            this.drawMltiPicControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 483);
            this.Controls.Add(this.drawMltiPicControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private DrawMltPicControl.DrawMltiPicControl drawMltiPicControl1;
    }
}