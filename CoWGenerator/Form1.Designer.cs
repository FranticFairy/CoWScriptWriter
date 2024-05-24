
namespace CoWGenerator
{
    partial class frmMain
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
            this.ofdCSVPicker = new System.Windows.Forms.OpenFileDialog();
            this.btnPickCSV = new System.Windows.Forms.Button();
            this.btnGO = new System.Windows.Forms.Button();
            this.fbdFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnLocation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ofdCSVPicker
            // 
            this.ofdCSVPicker.FileName = "ofdCSVPicker";
            // 
            // btnPickCSV
            // 
            this.btnPickCSV.Location = new System.Drawing.Point(13, 13);
            this.btnPickCSV.Name = "btnPickCSV";
            this.btnPickCSV.Size = new System.Drawing.Size(90, 25);
            this.btnPickCSV.TabIndex = 0;
            this.btnPickCSV.Text = "Pick CSV";
            this.btnPickCSV.UseVisualStyleBackColor = true;
            this.btnPickCSV.Click += new System.EventHandler(this.btnPickCSV_Click);
            // 
            // btnGO
            // 
            this.btnGO.Location = new System.Drawing.Point(13, 75);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(90, 25);
            this.btnGO.TabIndex = 1;
            this.btnGO.Text = "GO";
            this.btnGO.UseVisualStyleBackColor = true;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            // 
            // btnLocation
            // 
            this.btnLocation.Location = new System.Drawing.Point(13, 44);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(90, 25);
            this.btnLocation.TabIndex = 2;
            this.btnLocation.Text = "Save Location";
            this.btnLocation.UseVisualStyleBackColor = true;
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLocation);
            this.Controls.Add(this.btnGO);
            this.Controls.Add(this.btnPickCSV);
            this.Name = "frmMain";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdCSVPicker;
        private System.Windows.Forms.Button btnPickCSV;
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.FolderBrowserDialog fbdFolderBrowser;
        private System.Windows.Forms.Button btnLocation;
    }
}

