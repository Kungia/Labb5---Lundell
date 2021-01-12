
namespace Labb5___Lundell
{
    partial class ImageScraper
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
            this.ExtractBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.Linkbox = new System.Windows.Forms.TextBox();
            this.ImagesBox = new System.Windows.Forms.TextBox();
            this.LinkLabel = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ImgCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ExtractBtn
            // 
            this.ExtractBtn.Location = new System.Drawing.Point(594, 78);
            this.ExtractBtn.Name = "ExtractBtn";
            this.ExtractBtn.Size = new System.Drawing.Size(75, 23);
            this.ExtractBtn.TabIndex = 0;
            this.ExtractBtn.Text = "Extract";
            this.ExtractBtn.UseVisualStyleBackColor = true;
            this.ExtractBtn.Click += new System.EventHandler(this.ExtractBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(594, 396);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 1;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // Linkbox
            // 
            this.Linkbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Linkbox.Location = new System.Drawing.Point(23, 50);
            this.Linkbox.Name = "Linkbox";
            this.Linkbox.Size = new System.Drawing.Size(646, 22);
            this.Linkbox.TabIndex = 2;
            // 
            // ImagesBox
            // 
            this.ImagesBox.Location = new System.Drawing.Point(23, 118);
            this.ImagesBox.Multiline = true;
            this.ImagesBox.Name = "ImagesBox";
            this.ImagesBox.ReadOnly = true;
            this.ImagesBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ImagesBox.Size = new System.Drawing.Size(646, 272);
            this.ImagesBox.TabIndex = 3;
            // 
            // LinkLabel
            // 
            this.LinkLabel.AutoSize = true;
            this.LinkLabel.Location = new System.Drawing.Point(23, 27);
            this.LinkLabel.Name = "LinkLabel";
            this.LinkLabel.Size = new System.Drawing.Size(341, 17);
            this.LinkLabel.TabIndex = 4;
            this.LinkLabel.Text = "Insert link to the page you want to scrape for images:";
            // 
            // ImgCount
            // 
            this.ImgCount.AutoSize = true;
            this.ImgCount.Location = new System.Drawing.Point(20, 396);
            this.ImgCount.Name = "ImgCount";
            this.ImgCount.Size = new System.Drawing.Size(0, 17);
            this.ImgCount.TabIndex = 5;
            // 
            // ImageScraper
            // 
            this.AcceptButton = this.ExtractBtn;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(695, 450);
            this.Controls.Add(this.ImgCount);
            this.Controls.Add(this.LinkLabel);
            this.Controls.Add(this.ImagesBox);
            this.Controls.Add(this.Linkbox);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.ExtractBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ImageScraper";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExtractBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.TextBox Linkbox;
        private System.Windows.Forms.TextBox ImagesBox;
        private System.Windows.Forms.Label LinkLabel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label ImgCount;
    }
}

