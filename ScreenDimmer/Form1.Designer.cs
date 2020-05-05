using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenDimmer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private NotifyIcon notifyIcon;

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
            this.components = new Container();
            this.notifyIcon = new NotifyIcon(this.components);

            this.SuspendLayout();

            this.notifyIcon.Text = "Screen Dimmer";
            this.notifyIcon.Visible = true;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.BackColor = Color.Black;
            this.Opacity = .5f;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            // Create a big form that span across all monitors
            int width = 0, height = 0;
            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                // Get the highest height of all screens
                height = (screen.Bounds.Height > height) ? screen.Bounds.Height : height;
                width += screen.Bounds.Width;
            }

            this.Size = new Size(width, height);
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Name = this.Text = "Screen Dimmer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }

        #endregion
    }
}

